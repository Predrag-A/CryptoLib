using System;
using System.IO;
using System.Reflection;
using System.Xml;
using System.Xml.Serialization;
using CryptoLib;

namespace CryptoApp.Classes
{

    [Serializable]
    public class Settings
    {

        #region Constructors

        private Settings()
        {
            Default();
        }

        #endregion

        #region Properties

        [XmlElement("FSW_Enabled")]
        public bool FswEnabled { get; set; }

        [XmlElement("Crypt/Decrypt")]
        public bool FswDecrypt { get; set; }

        [XmlElement("FSW_Input_Path")]
        public string FswInput { get; set; }

        [XmlElement("FSW_Output_Path")]
        public string FswOutput { get; set; }

        [XmlElement("Algorithm_Type")]
        public Algorithm Algo { get; set; }

        [XmlElement("Process_Closed_Date")]
        public DateTime ProcessClosedDate { get; set; }

        [XmlElement("DT_Column_Key")]
        public string DTColKey { get; set; }

        [XmlElement("DT_Row_Key")]
        public string DTRowKey { get; set; }

        [XmlElement("XTEA_Key")]
        public string XTEAKey { get; set; }

        [XmlElement("XTEA_Rounds")]
        public uint XTEARounds { get; set; }

        [XmlArray("Knapsack_Private_Key")]
        public uint[] KSPrivateKey { get; set; }

        [XmlElement("Knapsack_Multiplier")]
        public uint KSm { get; set; }

        [XmlElement("Knapsack_Multiplier_Inverse")]
        public uint KSmInverse { get; set; }

        [XmlElement("Knapsack_Array_Continuation")]
        public uint KSn { get; set; }

        #endregion

        #region Methods

        public void Default()
        {
            FswEnabled = false;
            FswDecrypt = false;
            FswInput = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            FswOutput = string.Copy(FswInput);
            Algo = Algorithm.DoubleTranposition;
            ProcessClosedDate = DateTime.Now;
            DTColKey = "POTATO";
            DTRowKey = "SPARTA";
            XTEAKey = "a1b2c3d4e5f6g7h8";
            XTEARounds = 32;
            KSPrivateKey = new uint[] { 2, 3, 7, 14, 30, 57, 120, 251 };
            KSn = 491;
            KSm = 41;
            KSmInverse = 12;
        }

        public bool Save(string path)
        {
            var xmlSerializer = new XmlSerializer(typeof(Settings));
            using (var fileWriter = new FileStream(path, FileMode.Create))
                xmlSerializer.Serialize(fileWriter, this);
            return true;
        }

        public bool Load(string path)
        {
            if (!File.Exists(path)) return false;
            var xmlSerializer = new XmlSerializer(typeof(Settings));
            using (var fileReader = XmlReader.Create(path))
            {
                var temp = (Settings) xmlSerializer.Deserialize(fileReader);
                FswEnabled = temp.FswEnabled;
                FswDecrypt = temp.FswDecrypt;
                FswInput = temp.FswInput;
                FswOutput = temp.FswOutput;
                Algo = temp.Algo;
                ProcessClosedDate = temp.ProcessClosedDate;
                DTColKey = temp.DTColKey;
                DTRowKey = temp.DTRowKey;
                XTEAKey = temp.XTEAKey;
                XTEARounds = temp.XTEARounds;
                KSPrivateKey = temp.KSPrivateKey;
                KSn = temp.KSn;
                KSm = temp.KSm;
                KSmInverse = temp.KSmInverse;
            }

            return true;
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
