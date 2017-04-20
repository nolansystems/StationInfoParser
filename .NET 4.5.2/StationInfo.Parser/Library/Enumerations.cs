using System.ComponentModel;

namespace StationInfo.Parser.Library
{
    public class Enumerations
    {
        /// <summary>
        /// M = METAR reporting station. Also Z=obsolete? site
        /// </summary>
        public enum METAR
        {
            [Description("Unknown")]
            Unknown,

            [Description("Active")]
            X,

            [Description("Obsolete")]
            Z,

            [Description("Empty")]
            Empty
        }

        /// <summary>
        /// N = NEXRAD(WSR-88D) Radar site
        /// </summary>
        public enum RADAR
        {
            [Description("Unknown")]
            Unknown,

            [Description("NEXRAD")]
            X,

            [Description("Empty")]
            Empty
        }

        /// <summary>
        /// V = Aviation-specific flag(V= AIRMET / SIGMET end point, A= ARTCC T= TAF U= T + V)
        /// </summary>
        public enum AviationFlag
        {
            [Description("Unknown")]
            Unknown,

            [Description("AIRMET/SIGMET")]
            V,

            [Description("ARTCC")]
            A,

            [Description("TAF")]
            T,

            [Description("AIRMET/SIGMET and TAF")]
            U,

            [Description("Empty")]
            Empty
        }

        /// <summary>
        /// U = Upper air(rawinsonde= X) or Wind Profiler(W) site
        /// </summary>
        public enum UpperAir
        {
            [Description("Unknown")]
            Unknown,

            [Description("Rawinsonde")]
            X,

            [Description("Wind Profiler")]
            W,

            [Description("Empty")]
            Empty
        }

        /// <summary>
        /// A = Auto(A = ASOS, W = AWOS, M = Meso, H = Human, G = Augmented)(H / G not yet impl.)
        /// </summary>
        public enum ObservationSystem
        {
            [Description("Unknown")]
            Unknown,

            [Description("ASOS")]
            A,

            [Description("AWOS")]
            W,

            [Description("MESO")]
            M,

            [Description("Human")]
            H,

            [Description("Augmented")]
            G,

            [Description("Empty")]
            Empty
        }

        /// <summary>
        /// C = Office type F=WFO/R=RFC/C=NCEP Center
        /// </summary>
        public enum OfficeType
        {
            [Description("Unknown")]
            Unknown,

            [Description("WFO")]
            F,

            [Description("RFC")]
            R,

            [Description("NCEP")]
            C,

            [Description("Empty")]
            Empty
        }
    }
}