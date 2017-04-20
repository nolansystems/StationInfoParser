using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StationInfo.Parser;
using StationInfo.Parser.Library;
using System;

namespace Testing.Unit
{
    [TestClass]
    public class StationParserTests
    {
        public StationParser _spParser;

        [TestInitialize]
        public void Setup()
        {
            _spParser = new StationParser();
        }

        #region Enumeration Parsing

        [TestMethod]
        public void ParseValueForUpperAir()
        {
            var textValue = "x ";
            _spParser.ParseValueForUpperAir(textValue)
                .ShouldBeEquivalentTo<Enumerations.UpperAir>(Enumerations.UpperAir.X);

            textValue = " w";
            _spParser.ParseValueForUpperAir(textValue)
                .ShouldBeEquivalentTo<Enumerations.UpperAir>(Enumerations.UpperAir.W);

            textValue = " ";
            _spParser.ParseValueForUpperAir(textValue)
                .ShouldBeEquivalentTo<Enumerations.UpperAir>(Enumerations.UpperAir.Empty);

            textValue = "empty";
            _spParser.ParseValueForUpperAir(textValue)
                .ShouldBeEquivalentTo<Enumerations.UpperAir>(Enumerations.UpperAir.Unknown);
        }

        [TestMethod]
        public void ParseValueForRADAR()
        {
            var textValue = "x ";
            _spParser.ParseValueForRADAR(textValue)
                .ShouldBeEquivalentTo<Enumerations.RADAR>(Enumerations.RADAR.X);

            textValue = " ";
            _spParser.ParseValueForRADAR(textValue)
                .ShouldBeEquivalentTo<Enumerations.RADAR>(Enumerations.RADAR.Empty);

            textValue = "empty";
            _spParser.ParseValueForRADAR(textValue)
                .ShouldBeEquivalentTo<Enumerations.RADAR>(Enumerations.RADAR.Unknown);
        }

        [TestMethod]
        public void ParseValueForOfficeType()
        {
            var textValue = "C ";
            _spParser.ParseValueForOfficeType(textValue)
                .ShouldBeEquivalentTo<Enumerations.OfficeType>(Enumerations.OfficeType.C);

            textValue = " f";
            _spParser.ParseValueForOfficeType(textValue)
                .ShouldBeEquivalentTo<Enumerations.OfficeType>(Enumerations.OfficeType.F);

            textValue = "r";
            _spParser.ParseValueForOfficeType(textValue)
                .ShouldBeEquivalentTo<Enumerations.OfficeType>(Enumerations.OfficeType.R);

            textValue = " ";
            _spParser.ParseValueForOfficeType(textValue)
                .ShouldBeEquivalentTo<Enumerations.OfficeType>(Enumerations.OfficeType.Empty);

            textValue = "afds";
            _spParser.ParseValueForOfficeType(textValue)
                .ShouldBeEquivalentTo<Enumerations.OfficeType>(Enumerations.OfficeType.Unknown);
        }

        [TestMethod]
        public void ParseValueForObservationSystem()
        {
            var textValue = "A";
            _spParser.ParseValueForObservationSystem(textValue)
                .ShouldBeEquivalentTo<Enumerations.ObservationSystem>(Enumerations.ObservationSystem.A);

            textValue = "G";
            _spParser.ParseValueForObservationSystem(textValue)
                .ShouldBeEquivalentTo<Enumerations.ObservationSystem>(Enumerations.ObservationSystem.G);

            textValue = "h";
            _spParser.ParseValueForObservationSystem(textValue)
                .ShouldBeEquivalentTo<Enumerations.ObservationSystem>(Enumerations.ObservationSystem.H);

            textValue = "   m   ";
            _spParser.ParseValueForObservationSystem(textValue)
                .ShouldBeEquivalentTo<Enumerations.ObservationSystem>(Enumerations.ObservationSystem.M);

            textValue = "w ";
            _spParser.ParseValueForObservationSystem(textValue)
                .ShouldBeEquivalentTo<Enumerations.ObservationSystem>(Enumerations.ObservationSystem.W);

            textValue = " ";
            _spParser.ParseValueForObservationSystem(textValue)
                .ShouldBeEquivalentTo<Enumerations.ObservationSystem>(Enumerations.ObservationSystem.Empty);

            textValue = "unknown";
            _spParser.ParseValueForObservationSystem(textValue)
                .ShouldBeEquivalentTo<Enumerations.ObservationSystem>(Enumerations.ObservationSystem.Unknown);
        }

        [TestMethod]
        public void ParseValueForAviationFlag()
        {
            var textValue = "A";
            _spParser.ParseValueForAviationFlag(textValue)
                .ShouldBeEquivalentTo<Enumerations.AviationFlag>(Enumerations.AviationFlag.A);

            textValue = "t";
            _spParser.ParseValueForAviationFlag(textValue)
                .ShouldBeEquivalentTo<Enumerations.AviationFlag>(Enumerations.AviationFlag.T);

            textValue = " u ";
            _spParser.ParseValueForAviationFlag(textValue)
                .ShouldBeEquivalentTo<Enumerations.AviationFlag>(Enumerations.AviationFlag.U);

            textValue = "   v";
            _spParser.ParseValueForAviationFlag(textValue)
                .ShouldBeEquivalentTo<Enumerations.AviationFlag>(Enumerations.AviationFlag.V);

            textValue = " ";
            _spParser.ParseValueForAviationFlag(textValue)
                .ShouldBeEquivalentTo<Enumerations.AviationFlag>(Enumerations.AviationFlag.Empty);

            textValue = "v  v";
            _spParser.ParseValueForAviationFlag(textValue)
                .ShouldBeEquivalentTo<Enumerations.AviationFlag>(Enumerations.AviationFlag.Unknown);
        }

        [TestMethod]
        public void ParseValueForMETAR()
        {
            var textValue = "x";
            _spParser.ParseValueForMETAR(textValue)
                .ShouldBeEquivalentTo<Enumerations.METAR>(Enumerations.METAR.X);

            textValue = "z";
            _spParser.ParseValueForMETAR(textValue)
                .ShouldBeEquivalentTo<Enumerations.METAR>(Enumerations.METAR.Z);

            textValue = " ";
            _spParser.ParseValueForMETAR(textValue)
                .ShouldBeEquivalentTo<Enumerations.METAR>(Enumerations.METAR.Empty);

            textValue = "zz";
            _spParser.ParseValueForMETAR(textValue)
                .ShouldBeEquivalentTo<Enumerations.METAR>(Enumerations.METAR.Unknown);
        }

        #endregion Enumeration Parsing

        #region Parsing

        [TestMethod]
        public void ParseText()
        {
            // Verify that string with spaces doesn't return a Station object
            var rawText = " ";
            _spParser.ParseText(rawText)
                .Should()
                .BeEmpty();

            // Verify null strings cause an exception to be thrown
            try
            {
                rawText = null;
                _spParser.ParseText(rawText);
            }
            catch (Exception ex)
            {
                ex.Should().BeOfType<ArgumentException>();
            }

            // Verify that incorrectly formatted text doesn't return a station
            rawText = "asd;fkajsdf;lkajsdf;lkj ;lkj ;lkj ;lk j;lkjsafiefnasd;f";
            _spParser.ParseText(rawText)
                .Should()
                .BeEmpty();

            // Verify that a comment line, one that starts with an exclamation doesn't return a station
            rawText = "!   ICAO = 4-character international id";
            _spParser.ParseText(rawText)
                .Should()
                .BeEmpty();

            // Verify that lines of incorrect length doesn't return a station
            rawText = "ALASKA             16-DEC-13   " +
                System.Environment.NewLine +
                "CD STATION         ICAO IATA  SYNOP LAT     LONG ELEV   M N  V U  A C";
            _spParser.ParseText(rawText)
                .Should()
                .BeEmpty();

            // Verify that a properly formatted line would return a station object and verify the data
            Station station = new Station()
            {
                Country = "US",
                Elevation = 198,
                IATA = "AUB",
                ICAO = "KAUB",
                Latitude = "32 36N",
                Longitude = "085 30W",
                METAR = Enumerations.METAR.X,
                AviationFlag = Enumerations.AviationFlag.Empty,
                ObservationSystem = Enumerations.ObservationSystem.Empty,
                OfficeType = Enumerations.OfficeType.Empty,
                Priority = 7,
                RADAR = Enumerations.RADAR.Empty,
                StateCountryName = string.Empty,
                StateProvinceAbbrev = "AL",
                StationName = "AUBURN UNIV. (AM",
                SYNOP = 0,
                UpperAir = Enumerations.UpperAir.Empty
            };
            rawText = "AL AUBURN UNIV. (AM KAUB  AUB          32 36N  085 30W  198   X                7 US";
            var collection = _spParser.ParseText(rawText);
            collection.Should()
                .HaveCount(1);

            // Need to figure out how to do this in Fluent Assertions
            Assert.AreEqual(collection[0], station);
        }

        #endregion Parsing
    }
}