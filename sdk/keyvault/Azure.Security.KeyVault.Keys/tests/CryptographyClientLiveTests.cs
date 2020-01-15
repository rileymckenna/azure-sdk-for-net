﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using Azure.Core.Testing;
using Azure.Identity;
using Azure.Security.KeyVault.Keys.Cryptography;
using NUnit.Framework;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace Azure.Security.KeyVault.Keys.Tests
{
    public class CryptographyClientLiveTests : KeysTestBase
    {
        public CryptographyClientLiveTests(bool isAsync)
            : base(isAsync)
        {

        }

        [SetUp]
        public void ClearChallengeCacheforRecord()
        {
            // in record mode we reset the challenge cache before each test so that the challenge call
            // is always made.  This allows tests to be replayed independently and in any order
            if (Mode == RecordedTestMode.Record || Mode == RecordedTestMode.Playback)
            {
                Client = GetClient();

                ChallengeBasedAuthenticationPolicy.AuthenticationChallenge.ClearCache();
            }
        }

        [Test]
        public async Task EncryptDecryptRoundTrip([EnumValues]EncryptionAlgorithm algorithm)
        {
            KeyVaultKey key = await CreateTestKey(algorithm);
            RegisterForCleanup(key.Name);

            CryptographyClient cryptoClient = GetCryptoClient(key.Id, forceRemote: true);

            byte[] data = new byte[32];
            Recording.Random.NextBytes(data);

            EncryptResult encResult = await cryptoClient.EncryptAsync(algorithm, data);

            Assert.AreEqual(algorithm, encResult.Algorithm);
            Assert.AreEqual(key.Id, encResult.KeyId);
            Assert.IsNotNull(encResult.Ciphertext);

            DecryptResult decResult = await cryptoClient.DecryptAsync(algorithm, encResult.Ciphertext);

            Assert.AreEqual(algorithm, decResult.Algorithm);
            Assert.AreEqual(key.Id, decResult.KeyId);
            Assert.IsNotNull(decResult.Plaintext);

            CollectionAssert.AreEqual(data, decResult.Plaintext);
        }

        [Test]
        public async Task WrapUnwrapRoundTrip([EnumValues(Exclude = new[] { nameof(KeyWrapAlgorithm.A128KW), nameof(KeyWrapAlgorithm.A192KW), nameof(KeyWrapAlgorithm.A256KW) })]KeyWrapAlgorithm algorithm)
        {
            KeyVaultKey key = await CreateTestKey(algorithm);
            RegisterForCleanup(key.Name);

            CryptographyClient cryptoClient = GetCryptoClient(key.Id, forceRemote: true);

            byte[] data = new byte[32];
            Recording.Random.NextBytes(data);

            WrapResult encResult = await cryptoClient.WrapKeyAsync(algorithm, data);

            Assert.AreEqual(algorithm, encResult.Algorithm);
            Assert.AreEqual(key.Id, encResult.KeyId);
            Assert.IsNotNull(encResult.EncryptedKey);

            UnwrapResult decResult = await cryptoClient.UnwrapKeyAsync(algorithm, encResult.EncryptedKey);

            Assert.AreEqual(algorithm, decResult.Algorithm);
            Assert.AreEqual(key.Id, decResult.KeyId);
            Assert.IsNotNull(decResult.Key);

            CollectionAssert.AreEqual(data, decResult.Key);
        }

        [Test]
        public async Task SignVerifyDataRoundTrip([EnumValues]SignatureAlgorithm algorithm)
        {
            KeyVaultKey key = await CreateTestKey(algorithm);
            RegisterForCleanup(key.Name);

            CryptographyClient cryptoClient = GetCryptoClient(key.Id, forceRemote: true);

            byte[] data = new byte[32];
            Recording.Random.NextBytes(data);

            using HashAlgorithm hashAlgo = algorithm.GetHashAlgorithm();

            byte[] digest = hashAlgo.ComputeHash(data);

            SignResult signResult = await cryptoClient.SignAsync(algorithm, digest);

            SignResult signDataResult = await cryptoClient.SignDataAsync(algorithm, data);

            Assert.AreEqual(algorithm, signResult.Algorithm);
            Assert.AreEqual(algorithm, signDataResult.Algorithm);

            Assert.AreEqual(key.Id, signResult.KeyId);
            Assert.AreEqual(key.Id, signDataResult.KeyId);

            Assert.NotNull(signResult.Signature);
            Assert.NotNull(signDataResult.Signature);

            VerifyResult verifyResult = await cryptoClient.VerifyAsync(algorithm, digest, signDataResult.Signature);

            VerifyResult verifyDataResult = await cryptoClient.VerifyDataAsync(algorithm, data, signResult.Signature);

            Assert.AreEqual(algorithm, verifyResult.Algorithm);
            Assert.AreEqual(algorithm, verifyDataResult.Algorithm);

            Assert.AreEqual(key.Id, verifyResult.KeyId);
            Assert.AreEqual(key.Id, verifyDataResult.KeyId);

            Assert.True(verifyResult.IsValid);
            Assert.True(verifyResult.IsValid);
        }

        [Test]
        public async Task SignVerifyDataStreamRoundTrip([EnumValues]SignatureAlgorithm algorithm)
        {
            KeyVaultKey key = await CreateTestKey(algorithm);
            RegisterForCleanup(key.Name);

            CryptographyClient cryptoClient = GetCryptoClient(key.Id, forceRemote: true);

            byte[] data = new byte[8000];
            Recording.Random.NextBytes(data);

            using MemoryStream dataStream = new MemoryStream(data);

            using HashAlgorithm hashAlgo = algorithm.GetHashAlgorithm();

            byte[] digest = hashAlgo.ComputeHash(dataStream);

            dataStream.Seek(0, SeekOrigin.Begin);

            SignResult signResult = await cryptoClient.SignAsync(algorithm, digest);

            SignResult signDataResult = await cryptoClient.SignDataAsync(algorithm, dataStream);

            Assert.AreEqual(algorithm, signResult.Algorithm);
            Assert.AreEqual(algorithm, signDataResult.Algorithm);

            Assert.AreEqual(key.Id, signResult.KeyId);
            Assert.AreEqual(key.Id, signDataResult.KeyId);

            Assert.NotNull(signResult.Signature);
            Assert.NotNull(signDataResult.Signature);

            dataStream.Seek(0, SeekOrigin.Begin);

            VerifyResult verifyResult = await cryptoClient.VerifyAsync(algorithm, digest, signDataResult.Signature);

            VerifyResult verifyDataResult = await cryptoClient.VerifyDataAsync(algorithm, dataStream, signResult.Signature);

            Assert.AreEqual(algorithm, verifyResult.Algorithm);
            Assert.AreEqual(algorithm, verifyDataResult.Algorithm);

            Assert.AreEqual(key.Id, verifyResult.KeyId);
            Assert.AreEqual(key.Id, verifyDataResult.KeyId);

            Assert.True(verifyResult.IsValid);
            Assert.True(verifyResult.IsValid);
        }

        // We do not test using ES256K below since macOS doesn't support it; various ideas to work around that adversely affect runtime code too much.

        [Test]
        public async Task LocalSignVerifyRoundTrip([EnumValues(Exclude = new[] { nameof(SignatureAlgorithm.ES256K) })]SignatureAlgorithm algorithm)
        {
#if NET461
            if (algorithm.GetEcKeyCurveName() != default)
            {
                Assert.Ignore("Creating JsonWebKey with ECDsa is not supported on net461.");
            }
#endif

#if NETFRAMEWORK
            if (algorithm.GetRsaSignaturePadding() == RSASignaturePadding.Pss)
            {
                Assert.Ignore("RSA-PSS signature padding is not supported on .NET Framework.");
            }
#endif

            KeyVaultKey key = await CreateTestKeyWithKeyMaterial(algorithm);
            RegisterForCleanup(key.Name);

            (CryptographyClient client, ICryptographyProvider remoteClient) = GetCryptoClient(key);

            byte[] data = new byte[32];
            Recording.Random.NextBytes(data);

            using HashAlgorithm hashAlgo = algorithm.GetHashAlgorithm();
            byte[] digest = hashAlgo.ComputeHash(data);

            // Sign locally...
            SignResult signResult = await client.SignAsync(algorithm, digest);

            Assert.AreEqual(algorithm, signResult.Algorithm);
            Assert.AreEqual(key.Key.Id, signResult.KeyId);
            Assert.NotNull(signResult.Signature);

            // ...and verify remotely.
            VerifyResult verifyResult = await remoteClient.VerifyAsync(algorithm, digest, signResult.Signature);

            Assert.AreEqual(algorithm, verifyResult.Algorithm);
            Assert.AreEqual(key.Key.Id, verifyResult.KeyId);
            Assert.IsTrue(verifyResult.IsValid);
        }

        [Test]
        public async Task LocalSignVerifyRoundTripOnFramework([EnumValues(nameof(SignatureAlgorithm.PS256), nameof(SignatureAlgorithm.PS384), nameof(SignatureAlgorithm.PS512))]SignatureAlgorithm algorithm)
        {
#if !NETFRAMEWORK
            // RSA-PSS is not supported on .NET Framework so recorded tests will fall back to the remote client.
            Assert.Ignore("RSA-PSS is supported on .NET Core so local tests will pass. This test method is to test that on .NET Framework RSA-PSS sign/verify attempts fall back to the remote client.");
#endif

            KeyVaultKey key = await CreateTestKeyWithKeyMaterial(algorithm);
            RegisterForCleanup(key.Name);

            (CryptographyClient client, ICryptographyProvider remoteClient) = GetCryptoClient(key);

            byte[] data = new byte[32];
            Recording.Random.NextBytes(data);

            using HashAlgorithm hashAlgo = algorithm.GetHashAlgorithm();
            byte[] digest = hashAlgo.ComputeHash(data);

            // Sign locally...
            SignResult signResult = await client.SignAsync(algorithm, digest);

            Assert.AreEqual(algorithm, signResult.Algorithm);
            Assert.AreEqual(key.Key.Id, signResult.KeyId);
            Assert.NotNull(signResult.Signature);

            // ...and verify remotely.
            VerifyResult verifyResult = await remoteClient.VerifyAsync(algorithm, digest, signResult.Signature);

            Assert.AreEqual(algorithm, verifyResult.Algorithm);
            Assert.AreEqual(key.Key.Id, verifyResult.KeyId);
            Assert.IsTrue(verifyResult.IsValid);
        }

        [Test]
        public async Task SignLocalVerifyRoundTrip([EnumValues(Exclude = new[] { nameof(SignatureAlgorithm.ES256K) })]SignatureAlgorithm algorithm)
        {
#if NET461
            if (algorithm.GetEcKeyCurveName() != default)
            {
                Assert.Ignore("Creating JsonWebKey with ECDsa is not supported on net461.");
            }
#endif

#if NETFRAMEWORK
            if (algorithm.GetRsaSignaturePadding() == RSASignaturePadding.Pss)
            {
                Assert.Ignore("RSA-PSS signature padding is not supported on .NET Framework.");
            }
#endif

            KeyVaultKey key = await CreateTestKey(algorithm);
            RegisterForCleanup(key.Name);

            CryptographyClient client = GetCryptoClient(key.Id);

            byte[] data = new byte[32];
            Recording.Random.NextBytes(data);

            using HashAlgorithm hashAlgo = algorithm.GetHashAlgorithm();
            byte[] digest = hashAlgo.ComputeHash(data);

            // Should sign remotely...
            SignResult signResult = await client.SignAsync(algorithm, digest);

            Assert.AreEqual(algorithm, signResult.Algorithm);
            Assert.AreEqual(key.Key.Id, signResult.KeyId);
            Assert.NotNull(signResult.Signature);

            // ...and verify locally.
            VerifyResult verifyResult = await client.VerifyAsync(algorithm, digest, signResult.Signature);

            Assert.AreEqual(algorithm, verifyResult.Algorithm);
            Assert.AreEqual(key.Key.Id, verifyResult.KeyId);
            Assert.IsTrue(verifyResult.IsValid);
        }

        [Test]
        public async Task SignLocalVerifyRoundTripFramework([EnumValues(nameof(SignatureAlgorithm.PS256), nameof(SignatureAlgorithm.PS384), nameof(SignatureAlgorithm.PS512))]SignatureAlgorithm algorithm)
        {
#if !NETFRAMEWORK
            // RSA-PSS is not supported on .NET Framework so recorded tests will fall back to the remote client.
            Assert.Ignore("RSA-PSS is supported on .NET Core so local tests will pass. This test method is to test that on .NET Framework RSA-PSS sign/verify attempts fall back to the remote client.");
#endif

            KeyVaultKey key = await CreateTestKey(algorithm);
            RegisterForCleanup(key.Name);

            CryptographyClient client = GetCryptoClient(key.Properties.Id);

            byte[] data = new byte[32];
            Recording.Random.NextBytes(data);

            using HashAlgorithm hashAlgo = algorithm.GetHashAlgorithm();
            byte[] digest = hashAlgo.ComputeHash(data);

            // Should sign remotely...
            SignResult signResult = await client.SignAsync(algorithm, digest);

            Assert.AreEqual(algorithm, signResult.Algorithm);
            Assert.AreEqual(key.Key.Id, signResult.KeyId);
            Assert.NotNull(signResult.Signature);

            // ...and verify locally.
            VerifyResult verifyResult = await client.VerifyAsync(algorithm, digest, signResult.Signature);

            Assert.AreEqual(algorithm, verifyResult.Algorithm);
            Assert.AreEqual(key.Key.Id, verifyResult.KeyId);
            Assert.IsTrue(verifyResult.IsValid);
        }

        private async Task<KeyVaultKey> CreateTestKey(EncryptionAlgorithm algorithm)
        {
            string keyName = Recording.GenerateId();

            switch (algorithm.ToString())
            {
                case EncryptionAlgorithm.Rsa15Value:
                case EncryptionAlgorithm.RsaOaepValue:
                case EncryptionAlgorithm.RsaOaep256Value:
                    return await Client.CreateKeyAsync(keyName, KeyType.Rsa);
                default:
                    throw new ArgumentException("Invalid Algorithm", nameof(algorithm));
            }
        }

        private async Task<KeyVaultKey> CreateTestKey(KeyWrapAlgorithm algorithm)
        {
            string keyName = Recording.GenerateId();

            switch (algorithm.ToString())
            {
                case KeyWrapAlgorithm.Rsa15Value:
                case KeyWrapAlgorithm.RsaOaepValue:
                case KeyWrapAlgorithm.RsaOaep256Value:
                    return await Client.CreateKeyAsync(keyName, KeyType.Rsa);
                default:
                    throw new ArgumentException("Invalid Algorithm", nameof(algorithm));
            }
        }

        private CryptographyClient GetCryptoClient(Uri keyId, bool forceRemote = false, TestRecording recording = null)
        {
            recording ??= Recording;

            CryptographyClient client = new CryptographyClient(keyId, recording.GetCredential(new DefaultAzureCredential()), recording.InstrumentClientOptions(new CryptographyClientOptions()), forceRemote);
            return InstrumentClient(client);
        }

        private (CryptographyClient, ICryptographyProvider) GetCryptoClient(KeyVaultKey key, TestRecording recording = null)
        {
            recording ??= Recording;

            CryptographyClient client = new CryptographyClient(key, recording.GetCredential(new DefaultAzureCredential()), recording.InstrumentClientOptions(new CryptographyClientOptions()));
            CryptographyClient clientProxy = InstrumentClient(client);

            ICryptographyProvider remoteClientProxy = null;
            if (client.RemoteClient is RemoteCryptographyClient remoteClient)
            {
                remoteClientProxy = InstrumentClient(remoteClient);
            }

            return (clientProxy, remoteClientProxy);
        }

        private async Task<KeyVaultKey> CreateTestKey(SignatureAlgorithm algorithm)
        {
            string keyName = Recording.GenerateId();

            switch (algorithm.ToString())
            {
                case SignatureAlgorithm.RS256Value:
                case SignatureAlgorithm.RS384Value:
                case SignatureAlgorithm.RS512Value:
                case SignatureAlgorithm.PS256Value:
                case SignatureAlgorithm.PS384Value:
                case SignatureAlgorithm.PS512Value:
                    return await Client.CreateKeyAsync(keyName, KeyType.Rsa);
                case SignatureAlgorithm.ES256Value:
                    return await Client.CreateEcKeyAsync(new CreateEcKeyOptions(keyName, false) { CurveName = KeyCurveName.P256 });
                case SignatureAlgorithm.ES256KValue:
                    return await Client.CreateEcKeyAsync(new CreateEcKeyOptions(keyName, false) { CurveName = KeyCurveName.P256K });
                case SignatureAlgorithm.ES384Value:
                    return await Client.CreateEcKeyAsync(new CreateEcKeyOptions(keyName, false) { CurveName = KeyCurveName.P384 });
                case SignatureAlgorithm.ES512Value:
                    return await Client.CreateEcKeyAsync(new CreateEcKeyOptions(keyName, false) { CurveName = KeyCurveName.P521 });
                default:
                    throw new ArgumentException("Invalid Algorithm", nameof(algorithm));
            }
        }

        private async Task<KeyVaultKey> CreateTestKeyWithKeyMaterial(SignatureAlgorithm algorithm)
        {
            string keyName = Recording.GenerateId();

            JsonWebKey keyMaterial = null;
            switch (algorithm.ToString())
            {
                case SignatureAlgorithm.PS256Value:
                case SignatureAlgorithm.PS384Value:
                case SignatureAlgorithm.PS512Value:
                case SignatureAlgorithm.RS256Value:
                case SignatureAlgorithm.RS384Value:
                case SignatureAlgorithm.RS512Value:
                    using (RSA rsa = RSA.Create())
                    {
                        RSAParameters rsaParameters = new RSAParameters
                        {
                            D = new byte[] { 0x8a, 0x5a, 0x7f, 0x16, 0x29, 0x95, 0x8b, 0x84, 0xeb, 0x8c, 0xba, 0x93, 0xad, 0xbf, 0x40, 0xa2,
                                             0xcc, 0xb9, 0xe9, 0xf8, 0xaa, 0x42, 0x78, 0x24, 0x5d, 0xdf, 0x99, 0xa1, 0x51, 0xd5, 0x1b, 0xaa,
                                             0xfe, 0x0a, 0xa2, 0x82, 0x49, 0xd3, 0x19, 0x9c, 0xfd, 0x48, 0x92, 0xcc, 0x44, 0x98, 0xaf, 0xbf,
                                             0x09, 0xf9, 0x4f, 0xff, 0xcc, 0x49, 0x75, 0x71, 0x27, 0xe1, 0xd8, 0xe2, 0xf2, 0xb7, 0x75, 0x5f,
                                             0x5b, 0x75, 0x75, 0xff, 0x9f, 0xaa, 0x0d, 0xb5, 0x9a, 0x49, 0xff, 0x0b, 0x85, 0xb7, 0x05, 0xb6,
                                             0x8b, 0xfb, 0x1c, 0x7b, 0x2b, 0xf8, 0xf7, 0x9d, 0xad, 0x4b, 0xe7, 0x30, 0x89, 0x13, 0x9d, 0x2b,
                                             0x7f, 0x40, 0x34, 0x3d, 0x8e, 0x38, 0x43, 0x84, 0x19, 0x67, 0xae, 0xab, 0x65, 0xa3, 0xfd, 0x01,
                                             0xcd, 0x2d, 0x5c, 0x87, 0x9f, 0xb7, 0x07, 0x98, 0x82, 0x74, 0x13, 0x69, 0xd1, 0xba, 0x6c, 0xea,
                                             0xf9, 0x54, 0x59, 0xa1, 0x3d, 0x8a, 0xaf, 0x4c, 0xa6, 0x22, 0xde, 0x2a, 0xe3, 0xc1, 0x68, 0x4e,
                                             0xc4, 0x5f, 0x49, 0xe6, 0x78, 0xb6, 0x7c, 0xa7, 0x90, 0xeb, 0xa2, 0x78, 0x93, 0xb4, 0xbb, 0xd2,
                                             0x59, 0x13, 0xe9, 0x20, 0xf5, 0x1a, 0xe5, 0x27, 0x27, 0x6c, 0x98, 0x9e, 0x20, 0x73, 0xc6, 0x61,
                                             0x4f, 0x01, 0x10, 0xf7, 0xb7, 0xe8, 0x17, 0x5f, 0x0e, 0x6b, 0x2b, 0x02, 0xf5, 0xe7, 0x4e, 0x16,
                                             0xcb, 0xd7, 0x6d, 0xb3, 0x80, 0x17, 0xac, 0xad, 0x5c, 0x48, 0x16, 0xf1, 0x2a, 0xf2, 0xde, 0x14,
                                             0xb4, 0x1b, 0x1a, 0x52, 0x11, 0x75, 0x05, 0xd8, 0x2e, 0x37, 0xe3, 0x31, 0xa5, 0x81, 0xa3, 0x29,
                                             0x20, 0xae, 0x6f, 0x52, 0xf6, 0xe4, 0xd1, 0xc2, 0x73, 0x3f, 0x2e, 0x56, 0x8a, 0xa1, 0xc3, 0x0c,
                                             0x4c, 0x1d, 0xb4, 0x77, 0xb9, 0x2a, 0xd4, 0x88, 0xc1, 0xb3, 0x3e, 0x2b, 0xd1, 0x98, 0x49, 0x8d },

                            DP = new byte[] { 0x05, 0x0c, 0x0c, 0xe9, 0x95, 0x77, 0x4d, 0x0f, 0x1e, 0x4a, 0x95, 0xbf, 0xcb, 0x0e, 0x03, 0x89,
                                              0x6f, 0xe6, 0x56, 0x54, 0xb6, 0x5a, 0x19, 0xdd, 0x7e, 0xde, 0x06, 0xce, 0xdc, 0x1b, 0x76, 0xa1,
                                              0xaa, 0x02, 0xa6, 0x77, 0x52, 0xa4, 0xbf, 0x4b, 0x18, 0x9a, 0x91, 0xc5, 0x86, 0x4a, 0xa2, 0x5f,
                                              0xcc, 0x2c, 0x3e, 0x18, 0x75, 0x75, 0xb3, 0xb4, 0x85, 0xbd, 0x6a, 0x75, 0x01, 0x88, 0xd7, 0xb6,
                                              0x63, 0xb8, 0x4e, 0xed, 0x69, 0x53, 0xb2, 0xb3, 0x80, 0xf3, 0x24, 0x4c, 0x18, 0x21, 0x18, 0xf5,
                                              0xd0, 0xea, 0xf3, 0x53, 0x49, 0x74, 0x5a, 0xc5, 0x07, 0xe6, 0xbc, 0xe0, 0x48, 0x6b, 0xa0, 0xcf,
                                              0x0e, 0x27, 0x80, 0xde, 0x3e, 0x65, 0x30, 0x1e, 0x8a, 0xcb, 0x7b, 0x55, 0xb1, 0xd4, 0x3e, 0xe8,
                                              0x3d, 0xb0, 0xf1, 0x2a, 0x5d, 0x63, 0x33, 0x05, 0x04, 0xc0, 0x52, 0x4d, 0x68, 0xff, 0x28, 0xf9, },

                            DQ = new byte[] { 0xcc, 0xf9, 0x20, 0x49, 0xfd, 0x71, 0x7b, 0x95, 0xb4, 0x6e, 0xb6, 0xdb, 0x3f, 0x99, 0x5c, 0x2a,
                                              0xf1, 0xf7, 0x17, 0x35, 0x29, 0xc7, 0x2a, 0x87, 0x6b, 0x0c, 0x8b, 0xad, 0x35, 0x00, 0xff, 0xa2,
                                              0xd8, 0x22, 0x75, 0x35, 0x3e, 0x6d, 0xd9, 0x3d, 0x39, 0x1d, 0x06, 0x65, 0x26, 0x08, 0x19, 0xb0,
                                              0xe7, 0xd7, 0x6d, 0xd0, 0xec, 0xc4, 0xe7, 0xcb, 0x2a, 0xe4, 0x2d, 0x78, 0x09, 0x9e, 0x5d, 0x86,
                                              0x8c, 0x85, 0x27, 0xb7, 0x4f, 0xed, 0x22, 0xe3, 0xe5, 0x7a, 0x0a, 0xc0, 0xe0, 0x6d, 0xe7, 0x6a,
                                              0x5c, 0x8c, 0xb6, 0x6a, 0x79, 0x72, 0x6d, 0x12, 0xa4, 0x65, 0x5b, 0xa0, 0xa9, 0xcb, 0x8d, 0x2b,
                                              0xeb, 0x1b, 0x81, 0x84, 0x26, 0xf7, 0x00, 0x49, 0x25, 0x4a, 0xc9, 0xda, 0x43, 0x60, 0x15, 0x47,
                                              0x65, 0x94, 0xe3, 0xb9, 0x0b, 0x00, 0xcb, 0x07, 0x3f, 0x5d, 0xdf, 0x19, 0x4b, 0x0f, 0x84, 0x17, },

                            Exponent = new byte[] { 0x01, 0x00, 0x01, },

                            InverseQ = new byte[] { 0xc2, 0xb4, 0x1c, 0x29, 0x19, 0x9e, 0x24, 0xe3, 0x38, 0xe0, 0x9b, 0x25, 0x12, 0x25, 0x5b, 0x5f,
                                                    0xdb, 0x45, 0x72, 0xe8, 0xbe, 0x25, 0x4e, 0xc7, 0x0d, 0x15, 0x05, 0x18, 0xa8, 0x47, 0xf7, 0x87,
                                                    0xa4, 0xa5, 0x02, 0xa5, 0xa0, 0x00, 0xd9, 0x98, 0xf2, 0xf4, 0x33, 0x64, 0x80, 0x30, 0xb9, 0x6c,
                                                    0xcc, 0x83, 0xc0, 0x7a, 0xf3, 0x32, 0xfd, 0x60, 0x91, 0x02, 0x61, 0x9e, 0x79, 0x68, 0xcd, 0x84,
                                                    0x6c, 0x39, 0x1e, 0x47, 0xb1, 0x13, 0xf9, 0xea, 0x2b, 0xc9, 0x65, 0xd5, 0x1d, 0x8a, 0x47, 0xf4,
                                                    0xa3, 0xf2, 0x01, 0x50, 0x0a, 0xad, 0x72, 0xcd, 0xe3, 0x19, 0x67, 0x3e, 0x15, 0x8d, 0x40, 0x7c,
                                                    0x8f, 0x30, 0xa0, 0xc9, 0x3b, 0xf9, 0x96, 0xba, 0x58, 0xae, 0xd6, 0xc8, 0x26, 0xa6, 0xa2, 0xa8,
                                                    0x0f, 0x96, 0x2b, 0x28, 0xf8, 0x39, 0x11, 0xa1, 0xf0, 0x6a, 0xc5, 0xdd, 0x99, 0x5b, 0x18, 0x1a, },

                            Modulus = new byte[] { 0xc7, 0x61, 0xab, 0x5f, 0xc0, 0x4c, 0x50, 0xdf, 0x3a, 0x21, 0x87, 0x41, 0x6b, 0x42, 0x3d, 0xbd,
                                                   0xd7, 0x81, 0xe5, 0xed, 0xc0, 0x59, 0xe6, 0xa0, 0xd0, 0xcc, 0x7e, 0xd7, 0xbe, 0x0f, 0xcd, 0xd5,
                                                   0x3d, 0x23, 0x08, 0xa2, 0x81, 0x94, 0xc8, 0x60, 0xd0, 0xfc, 0xe8, 0xf6, 0xdf, 0x22, 0xe8, 0xa1,
                                                   0xae, 0x4c, 0xab, 0x78, 0xe6, 0x7d, 0x65, 0x1c, 0x20, 0x1a, 0x7b, 0xf2, 0xd9, 0x10, 0xa2, 0x85,
                                                   0x28, 0x81, 0xc0, 0x1d, 0x4d, 0xc6, 0xf0, 0x4f, 0x36, 0xe0, 0x83, 0x14, 0x4c, 0x30, 0x5c, 0xef,
                                                   0x9c, 0x93, 0x26, 0x7f, 0xf4, 0x67, 0x93, 0x47, 0xe8, 0x3b, 0x27, 0x91, 0xfb, 0xe9, 0xfd, 0xbb,
                                                   0x67, 0x9a, 0xa6, 0x0f, 0x84, 0x47, 0xae, 0x55, 0x55, 0x38, 0x32, 0x68, 0xfc, 0x97, 0x42, 0xc1,
                                                   0x77, 0x4e, 0x7c, 0x8e, 0xc2, 0x24, 0xe9, 0x9c, 0x29, 0x12, 0xf6, 0xce, 0x90, 0xa3, 0x77, 0x05,
                                                   0xaa, 0xc7, 0x63, 0x62, 0x3b, 0x38, 0xf6, 0xee, 0x77, 0x46, 0x0b, 0xed, 0xca, 0xca, 0x6e, 0x6e,
                                                   0x08, 0xd1, 0x5e, 0xa9, 0xd1, 0x86, 0xea, 0xdf, 0xcc, 0x7c, 0x17, 0x9c, 0xf2, 0xae, 0x4c, 0x02,
                                                   0xe8, 0x47, 0xcf, 0x95, 0xf0, 0x7c, 0x2f, 0x6f, 0x9b, 0x97, 0x87, 0xe7, 0x98, 0xf4, 0x07, 0x4d,
                                                   0xd5, 0x2d, 0xf3, 0x5e, 0x91, 0x52, 0x57, 0x99, 0xbd, 0x54, 0xb5, 0x04, 0xef, 0xc8, 0x14, 0x0b,
                                                   0xe0, 0xb3, 0x0b, 0x72, 0xb8, 0x43, 0x0e, 0x1f, 0x13, 0x79, 0xa1, 0x88, 0xc9, 0x96, 0x39, 0x68,
                                                   0xcb, 0x16, 0x2c, 0xfd, 0xa4, 0x5f, 0x3a, 0x0d, 0x31, 0xd8, 0xc1, 0x12, 0x02, 0xf7, 0x3b, 0x6c,
                                                   0xa1, 0x30, 0xad, 0x6d, 0xa4, 0xcf, 0x42, 0xe2, 0xb6, 0x5c, 0xdc, 0x33, 0xf5, 0x17, 0xda, 0x3a,
                                                   0x66, 0xdf, 0xdf, 0x6a, 0x04, 0x51, 0x84, 0x8b, 0x3d, 0x8b, 0x7b, 0x9f, 0x7a, 0xd7, 0xdf, 0xad, },

                            P = new byte[] { 0xca, 0x2e, 0x9f, 0xdd, 0x52, 0x22, 0x31, 0xd3, 0x75, 0x50, 0x1f, 0xab, 0x00, 0x48, 0x62, 0xcf,
                                             0x17, 0x1f, 0xa5, 0x17, 0xe4, 0x46, 0x22, 0xfc, 0xfc, 0x2b, 0x73, 0x4e, 0xb2, 0x1f, 0xb9, 0x72,
                                             0xa4, 0xaa, 0x52, 0xee, 0x05, 0xa4, 0xeb, 0x51, 0xfa, 0xa0, 0x9b, 0x3d, 0xf9, 0xc4, 0x04, 0x22,
                                             0xd2, 0xf0, 0xb4, 0xff, 0xee, 0x2f, 0xc0, 0x81, 0x2a, 0x3d, 0x5e, 0xe4, 0x75, 0x55, 0xf2, 0x1c,
                                             0x13, 0xd3, 0x59, 0x26, 0x38, 0x81, 0x91, 0xb6, 0xb6, 0x8e, 0x47, 0x3c, 0x27, 0x9e, 0x87, 0x07,
                                             0x2d, 0xcf, 0xd7, 0xaa, 0x5f, 0x15, 0x50, 0xf5, 0xc7, 0x01, 0x5f, 0x9c, 0xae, 0x9d, 0xec, 0x63,
                                             0xfc, 0x04, 0xda, 0xb7, 0xe7, 0x80, 0x14, 0x9c, 0xef, 0x6a, 0xbd, 0x36, 0x02, 0xce, 0xaa, 0xf3,
                                             0x93, 0x46, 0x8c, 0xb9, 0x0b, 0x82, 0xe1, 0x5d, 0x39, 0xcb, 0x46, 0x5f, 0xa7, 0xd5, 0xbe, 0xef, },

                            Q = new byte[] { 0xfc, 0x74, 0x33, 0xc3, 0x32, 0x64, 0x9f, 0x78, 0xe7, 0xfd, 0x79, 0xc0, 0xb0, 0x60, 0x1f, 0x94,
                                             0xc3, 0x3d, 0xd9, 0xfc, 0x02, 0xf7, 0x16, 0x2d, 0x47, 0x88, 0xfc, 0xf4, 0x13, 0xa3, 0xbf, 0x25,
                                             0x80, 0x3c, 0x1b, 0x1d, 0x12, 0x43, 0x5c, 0xce, 0x22, 0xa4, 0x01, 0x7e, 0x04, 0x7a, 0xf3, 0x11,
                                             0x66, 0x36, 0x49, 0x3c, 0x3b, 0x6f, 0x49, 0x69, 0x74, 0xd5, 0x35, 0x23, 0x90, 0x47, 0xb3, 0x15,
                                             0xe7, 0xa5, 0x26, 0x48, 0x9d, 0xb5, 0x38, 0xa0, 0x44, 0x58, 0x63, 0xb2, 0xdd, 0x94, 0xaf, 0x2e,
                                             0x42, 0x08, 0x25, 0x19, 0x4a, 0x7b, 0xe5, 0x72, 0xbe, 0xd5, 0xa3, 0x92, 0x0b, 0xba, 0xf7, 0x5f,
                                             0x0b, 0x18, 0xa6, 0x62, 0x19, 0x6d, 0x53, 0xc1, 0x8a, 0x86, 0x19, 0x43, 0x53, 0xa4, 0x3a, 0x53,
                                             0x94, 0xd9, 0x99, 0x8b, 0x3a, 0xe4, 0x1e, 0xc5, 0x86, 0x15, 0x89, 0x53, 0xb5, 0x3d, 0x8b, 0x23, },
                        };

                        rsa.ImportParameters(rsaParameters);

                        keyMaterial = new JsonWebKey(rsa, includePrivateParameters: true);
                    }

                    break;

                case SignatureAlgorithm.ES256Value:
                case SignatureAlgorithm.ES256KValue:
                case SignatureAlgorithm.ES384Value:
                case SignatureAlgorithm.ES512Value:
#if NET461
                    Assert.Ignore("Creating JsonWebKey with ECDsa is not supported on net461.");
#else
                    KeyCurveName curveName = algorithm.GetEcKeyCurveName();
                    ECCurve curve = ECCurve.CreateFromOid(curveName.Oid);

                    using (ECDsa ecdsa = ECDsa.Create())
                    {
                        try
                        {
                            ecdsa.GenerateKey(curve);
                            keyMaterial = new JsonWebKey(ecdsa, includePrivateParameters: true);
                        }
                        catch (NotSupportedException)
                        {
                            Assert.Inconclusive("This platform does not support OID {0}", curveName.Oid);
                        }
                    }
#endif

                    break;

                default:
                    throw new ArgumentException("Invalid Algorithm", nameof(algorithm));
            }

            KeyVaultKey key = await Client.ImportKeyAsync(keyName, keyMaterial);

            keyMaterial.Id = key.Key.Id;
            key.Key = keyMaterial;

            return key;
        }
    }
}
