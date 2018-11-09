﻿using System;
using System.IO;
using System.Reflection;
using System.Xml;
using System.Xml.Serialization;

namespace CryptoApp.Classes
{
    public enum Algorithm
    {
        DoubleTranposition,
        XTEA,
        OFB,
        Knapsack,
        MD5
    }

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

        [XmlElement("FSW_Input_Path")]
        public string FswInput { get; set; }

        [XmlElement("FSW_Output_Path")]
        public string FswOutput { get; set; }

        [XmlElement("Algorithm_Type")]
        public Algorithm Algo { get; set; }


        #endregion

        #region Methods

        public void Default()
        {
            FswEnabled = false;
            FswInput = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            FswOutput = string.Copy(FswInput);
            Algo = Algorithm.DoubleTranposition;
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
                FswInput = temp.FswInput;
                FswOutput = temp.FswOutput;
                Algo = temp.Algo;
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
