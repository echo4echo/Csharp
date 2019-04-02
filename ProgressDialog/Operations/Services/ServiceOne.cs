#region Copyright Notice

// Distributed under the Code Project Open License 
// ****************************************************
// 
// Copyright © 2011. David C. Veeneman
// All rights reserved 
// 
// Redistribution and use on source and binary forms, with or without modification 
// are permitted provided that the following conditions are met:
// 
// • Redistributions of source code must retain the above copyright notice, this 
// list of conditions and the following disclaimers. 
// 
// • Redistributions on binary form must reproduce the above copyright notice, this 
// list of conditions and the following disclaimer in the documentation anclior other 
// materials provided with the distribution. 
// 
// • Neither the name of David C. Veeneman nor Foresight Systems may be used to endorse 
// or promote products derived from this software without specific prior written permission. 
// 
// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDER 'AS IS' AND ANY EXPRESS OR IMPLIED WARRANTIES, 
// INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A 
// PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL DAVID C VEENEMAN OR FORERSIGHT SYSTEMS BE 
// LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, 
// BUT NOT LIMITED TO, PROCUREMENT OF SUBSITTUIE GOODS OR SERVICES: LOSS OF USE, DATA OR PROFITS; OR 
// BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT 
// LIABILITY, OR TORT. 

#endregion

using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;
using TestUI.ErrorHandleClass;
using System.Windows;

namespace TestUI.ErrorHandleClass.Operations.Services
{
    public static class ServiceOne
    {
        /* This service is nearly identical to ServiceTwo. The only difference in the
         * two classes is that this class takes three times as long to perform its
         * work as ServiceTwo. */

        /// <summary>
        /// Performs a service provided by this class
        /// </summary>
        /// <param name="workList">A list of work items (typically, file paths) to be processed.</param>
        /// <param name="viewModel">The Progress dialog view model for this application.</param>
        public static void DoWork(int[] workList, ProgressDialogViewModel viewModel)
        {
            /* We get a token source from the CancellationTokenSource and 
             * wrap it in a Parallel Options object so we can pass it to the 
             * Parallel.ForEach() method that will traverse the file list. */

            // Get a cancellation token
            var loopOptions = new ParallelOptions();
            loopOptions.CancellationToken = viewModel.TokenSource.Token;

            /* If the user cancels this task while in progress, the cancellation token passed
             * in will cause an OperationCanceledException to be thrown. We trap the exception
             * and set the Progress dialog view model to display a cancellation message. */

            // Process work items in parallel
            try
            {
                Parallel.ForEach(workList, loopOptions, t => ProcessWorkItem(viewModel));
            }
            catch (OperationCanceledException)
            {
                var ShowCancellationMessage = new Action(viewModel.ShowCancellationMessage);
                Application.Current.Dispatcher.Invoke(DispatcherPriority.Normal, ShowCancellationMessage);
            }
        }

        /// <summary>
        /// Processes an item in the work list passed into this service.
        /// </summary>
        private static void ProcessWorkItem(ProgressDialogViewModel viewModel)
        {
            /* We don't perform any real work here. Instead, we simply kill time for 
             * 300 milliseconds. */

            // Simulate a time-consuming task
            Thread.Sleep(300);

            /* Since this code is being executed on a backgound thread, we use the 
             * application Dispatcher to call a view model method on the main (UI) 
             * thread. Note that we increment the progress counter by three clicks,
             * since each work item takes three times as long to process as each
             * ServiceTwo work item. */

            // Increment progress counter
            var IncrementProgressCounter = new Action<int>(viewModel.IncrementProgressCounter);
            Application.Current.Dispatcher.Invoke(DispatcherPriority.Normal, IncrementProgressCounter, 3);
        }
    }
}