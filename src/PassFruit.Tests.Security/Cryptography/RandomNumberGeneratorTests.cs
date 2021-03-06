﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using PassFruit.Security.Cryptography;

namespace PassFruit.Tests.Security.Cryptography
{
    [TestFixture]
    public abstract class RandomNumberGeneratorTests
    {

        protected abstract RandomNumberGenerator CreateRandomNumberGenerator();

        private const int KnownSizeInBits = 256;

        [Test]
        public void WhenGeneratingARandomNumber_TheCorrectSizeShouldBeReturned()
        {
            // Given
            var rng = CreateRandomNumberGenerator();

            // When
            var randomNumber = rng.Generate(KnownSizeInBits);

            // Then
            randomNumber.Length.Should().Be(KnownSizeInBits / 8);
            randomNumber.Should().NotBeEmpty();
        }

        [Test]
        public void WhenGeneratingTwoRandomNumbers_ThenTheyShouldBeDifferent()
        {
            // Given
            var rng = CreateRandomNumberGenerator();

            // When
            var firstRandomNumber = rng.Generate(KnownSizeInBits);
            var secondRandomNumber = rng.Generate(KnownSizeInBits);

            // Then
            firstRandomNumber.Should().NotBeEquivalentTo(secondRandomNumber);
        }

        [Test]
        public void WhenGeneratingTwoRandomNumbersWithDifferentSizes_TheyShouldBeDifferent()
        {
            // Given
            var rng = CreateRandomNumberGenerator();

            // When
            var longRandomNumber = rng.Generate(1024);
            var shortRandomNumber = rng.Generate(128);

            // Then
            longRandomNumber.Length.Should().BeGreaterThan(shortRandomNumber.Length);
        }

        [Test]
        public void WheInvalidNumberOfBitsAreRequested_AnErrorShouldBeThrown()
        {
            // Given
            var rng = CreateRandomNumberGenerator();

            // When
            Action generateNegativeBits = () => rng.Generate(-1);
            Action generateZeroBits = () => rng.Generate(0);
            Action generate127Bits = () => rng.Generate(127);

            // Then
            generateNegativeBits.ShouldThrow<ArgumentException>();
            generateZeroBits.ShouldThrow<ArgumentException>();
            generate127Bits.ShouldThrow<ArgumentException>();
        }

    }
}
