using System.IO;
using System.Reflection;

namespace CryptoApp.Classes
{
    internal enum Algorithm
    {
        DoubleTranposition,
        XTEA,
        CFB,
        Knapsack,
        MD5
    }

    internal class Settings
    {

        #region Constructors

        private Settings()
        {
            Default();
        }

        #endregion

        #region Properties

        public bool FswEnabled { get; set; }

        public string FswPath { get; set; }

        public Algorithm Algo { get; set; }


        #endregion

        #region Methods

        public void Default()
        {
            FswEnabled = false;
            FswPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            Algo = Algorithm.DoubleTranposition;
        }

        #endregion

        #region Singleton

        private static Settings _instance;

        public static Settings Instance
        {
            get => _instance ?? (_instance = new Settings());

            set => _instance = value;
        }

        #endregion
    }
}
