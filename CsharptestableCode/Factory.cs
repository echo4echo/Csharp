/////////////////////////////////////////////////////////////////////////////////////
///        Name space: NibotUI.Business
///        File name : Factory
///        Description: Creat the Factory design
///        Version Nr:
///        Created data : 4/17/2019 1:26:25 PM
///        
///        Author: Han Liu
///        Copyright (c) : MIT
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

namespace CsharptestableCode
{
    /// <summary>
    /// Interface of the Factory
    /// </summary>
    public interface ISaveFactory
    {

    }

    /// <summary>
    /// The Creator Abstract Class
    /// </summary>
    public abstract class SettingFactory
    {
        public abstract ISaveFactory GetSavedClass(string theType);

    }

    /// <summary>
    /// A 'ConcreteCreator' class
    /// </summary>
    public class ConcreteSettingFactory : SettingFactory
    {
        public override ISaveFactory GetSavedClass(string Vehicle)
        {
            switch (Vehicle)
            {
                case "1":
                    return new function1();
                case "2":
                    return new function2();
                default:
                    throw new ApplicationException(string.Format("Vehicle '{0}' cannot be created", Vehicle));
            }
        }

    }


    public class function1 : ISaveFactory
    {
        
    }

    public class function2 : ISaveFactory
    {
        
    }
    
}
