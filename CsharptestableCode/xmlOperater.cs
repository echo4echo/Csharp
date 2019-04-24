/////////////////////////////////////////////////////////////////////////////////////
///        Name space: NibotUI.Helper
///        File name : xmlOperater
///        Description: This file show how to write a testable for save load xml
///			IXMLOperator: interface which is used by others function;
///			
///        Version Nr:
///        Created data : 4/24/2019 9:11:14 AM
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

using Microsoft.Win32;
using System.IO;

namespace CsharptestableCode
{
    interface IXMLOperator
    {
        /// <summary>
        /// Save the data as XML file
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        bool XML_File_Save(object data, string name, bool manulloadflag);

        /// <summary>
        /// Load  all kind of object data XML file from PC by using Factory design pattern
        /// </summary>
        /// <param name="data"></param>
        /// <param name="loadDialogFlag"></param>
        /// <returns></returns>
        /// 
        bool XML_File_Load(ref ISaveFactory data, bool manulloadflag);
    }

    class xmlOperater : IXMLOperator
    {
        //Desktop directory
        private string initialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop).ToString();
        //System App1, auto load directory
        private string systemApp1 = AppDomain.CurrentDomain.BaseDirectory + "System\\" + "systemApp1" + ".xml";
		
		
		public void xmlOperater()
		{
			
		}

        /// <summary>
        /// Save data with name
        /// </summary>
        /// <param name="data"></param>
        /// <param name="name"></param>
        /// <param name="manulloadflag"> manual save or autosave</param>
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
                    FileStream file = File.Create(systemApp1);
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
        /// <param name="manulloadflag"> manual load or auto load</param>
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
                    StreamReader file = new StreamReader(systemApp1);
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
	
	//How to use
	void Main()
	{
		private IXMLOperator _xmlOp = new xmlOperater();
		
		//
		//blablabla
		//
	}
}
