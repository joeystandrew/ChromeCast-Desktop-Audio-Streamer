﻿using System;
using Microsoft.Practices.Unity;
using ChromeCast.Desktop.AudioStreamer.Application;
using ChromeCast.Desktop.AudioStreamer.Application.Interfaces;
using ChromeCast.Desktop.AudioStreamer.Communication;
using ChromeCast.Desktop.AudioStreamer.Discover;
using ChromeCast.Desktop.AudioStreamer.Streaming;
using ChromeCast.Desktop.AudioStreamer.Streaming.Interfaces;
using ChromeCast.Desktop.AudioStreamer.Communication.Interfaces;
using ChromeCast.Desktop.AudioStreamer.Discover.Interfaces;
using ChromeCast.Desktop.AudioStreamer.Classes;
using System.Windows.Forms;

namespace ChromeCast.Desktop.AudioStreamer
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            AppDomain currentDomain = AppDomain.CurrentDomain;
            currentDomain.UnhandledException += new UnhandledExceptionEventHandler(UnhandledHandler);

            DependencyFactory.Container
                .RegisterType<ILogger, Logger>(new ContainerControlledLifetimeManager())
                .RegisterType<IApplicationLogic, ApplicationLogic>(new ContainerControlledLifetimeManager())
                .RegisterType<IMainForm, MainForm>(new ContainerControlledLifetimeManager())
                .RegisterType<IDevices, Devices>(new ContainerControlledLifetimeManager())
                .RegisterType<IDiscoverDevices, DiscoverDevices>()
                .RegisterType<IChromeCastMessages, ChromeCastMessages>()
                .RegisterType<IDeviceConnection, DeviceConnection>()
                .RegisterType<IDeviceCommunication, DeviceCommunication>()
                .RegisterType<IStreamingConnection, StreamingConnection>()
                .RegisterType<IDeviceReceiveBuffer, DeviceReceiveBuffer>()
                .RegisterType<ILoopbackRecorder, LoopbackRecorder>()
                .RegisterType<IDeviceStatusTimer, DeviceStatusTimer>()
                .RegisterType<IConfiguration, Configuration>()
                .RegisterType<IStreamingRequestsListener, StreamingRequestsListener>()
                .RegisterType<IAudioHeader, AudioHeader>()
                .RegisterType<IDevice, Device>();

            System.Windows.Forms.Application.EnableVisualStyles();
            System.Windows.Forms.Application.SetCompatibleTextRenderingDefault(false);
            System.Windows.Forms.Application.Run(DependencyFactory.Container.Resolve<MainForm>());
        }

        private static void UnhandledHandler(object sender, UnhandledExceptionEventArgs e)
        {
#if DEBUG
            Exception exception = (Exception)e.ExceptionObject;
            MessageBox.Show(exception.Message);
#endif
        }
    }
}
