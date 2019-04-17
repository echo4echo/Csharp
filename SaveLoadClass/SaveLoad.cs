/////////////////////////////////////////////////////////////////////////////////////
///        Name space: NibotUI.Business
///        File name : SaveLoad
///        Description:
///        By using the factory design pattern to realize a dummy ref obj
///        Notes: ref object is not possible!
///        Version Nr:
///        Created data : 4/17/2019 1:31:52 PM
///        
///        Author: Han Liu
///        Copyright (c) MIT
///        
/// Version History:
///*********************************************************************************
///        Date       Version        Author          Description
///*********************************************************************************
///   | xx.xx.xxxx |    1.0    |     Han Liu   |  Initial Version:
///  
/// //////////////////////////////////////////////////////////////////////////////////


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NibotUI.NibotRelevant;
using Microsoft.Win32;
using System.IO;

namespace App
{
    class SaveLoad
    {

        private string initialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop).ToString();
        private string systemSettingPath = AppDomain.CurrentDomain.BaseDirectory + "System\\" + "1" + ".xml";
        private string systemPickPlacePath = AppDomain.CurrentDomain.BaseDirectory + "System\\" + "2" + ".xml";

        /// <summary>
        /// Object save function, the object type is not sensitive
        /// </summary>
        /// <param name="data"></param>
        /// <param name="name"></param>
        /// <param name="manulloadflag"></param>
        /// <returns></returns>
        public bool XML_File_Save(object data, string name, bool manulloadflag)
        {
            if (manulloadflag)// manual save
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "XML file (*.xml)|*.xml";
                saveFileDialog.InitialDirectory = initialDirectory;
                saveFileDialog.FileName = name + ".xml";
                if (saveFileDialog.ShowDialog() == true)
                {
                    try
                    {
                        //Mouse.OverrideCursor = Cursors.Wait;
                        System.Xml.Serialization.XmlSerializer xmlWriter = new System.Xml.Serialization.XmlSerializer(data.GetType());
                        FileStream file = File.Create(saveFileDialog.FileName);
                        xmlWriter.Serialize(file, data);
                        file.Close();
                        //Mouse.OverrideCursor = null;
                        return true;
                    }
                    catch
                    {
                        //throw;
                        return false;
                    }
                }
            }
            else //auto save into sys
            {
                try
                {
                    //Mouse.OverrideCursor = Cursors.Wait;
                    System.Xml.Serialization.XmlSerializer xmlWriter = new System.Xml.Serialization.XmlSerializer(data.GetType());
                    FileStream file = File.Create(systemPickPlacePath);
                    xmlWriter.Serialize(file, data);
                    file.Close();
                    //Mouse.OverrideCursor = null;
                    return true;
                }
                catch
                {
                    //throw;
                    return false;
                }
            }
            return false;
        }


        /// <summary>
        /// Object load function, must use the factroy interface to access the saving data
        /// The safety of this function need to be checked.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="manulloadflag"></param>
        /// <returns></returns>
        public bool XML_File_Load(ref ISaveFactory data, bool manulloadflag)
        {
            if (manulloadflag) // manual laod the file
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "XML file (*.xml)|*.xml";
                openFileDialog.InitialDirectory = initialDirectory;
                if (openFileDialog.ShowDialog() == true)
                {
                    try
                    {
                        //Mouse.OverrideCursor = Cursors.Wait;
                        System.Xml.Serialization.XmlSerializer xmlReader = new System.Xml.Serialization.XmlSerializer(data.GetType());
                        StreamReader file = new StreamReader(openFileDialog.FileName);
                        data = (ISaveFactory)xmlReader.Deserialize(file);
                        file.Close();
                        //Mouse.OverrideCursor = null;
                        return true;
                    }
                    catch
                    {
                        //throw;
                        return false;
                    }
                }
            }
            else
            {
                try
                {
                    //Mouse.OverrideCursor = Cursors.Wait;
                    System.Xml.Serialization.XmlSerializer xmlReader = new System.Xml.Serialization.XmlSerializer(data.GetType());
                    StreamReader file = new StreamReader(systemPickPlacePath);
                    data = (ISaveFactory)xmlReader.Deserialize(file);
                    file.Close();
                    //Mouse.OverrideCursor = null;
                    return true;
                }
                catch
                {
                    //throw;
                    return false;
                }
            }
            return false;
        }

    }
}
