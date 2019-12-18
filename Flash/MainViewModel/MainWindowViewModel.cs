/////////////////////////////////////////////////////////////////////////////////////
///        Name space: NiFlash.MainViewModel
///        File name : MainWindowViewModel
///        Description:
///        Version Nr:
///        Created data : 12/16/2019 2:50:10 PM
///        
///        Author: Han Liu
///        Copyright (c) 2018 All Rights Reserved  
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
using GalaSoft.MvvmLight;

namespace NiFlash.MainViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {

        private List<string> _positionCtrlStepSize;
        public List<string> PositionCtrlStepSize
        {
            get { return _positionCtrlStepSize; }
            set { Set(ref _positionCtrlStepSize, value); }
        }
    }
}
