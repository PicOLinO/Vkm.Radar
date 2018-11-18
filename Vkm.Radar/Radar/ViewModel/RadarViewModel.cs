﻿using System.Collections.ObjectModel;
using System.Timers;
using DevExpress.Mvvm;
using Vkm.Radar.Radar.RadarComponents.ViewModel;

namespace Vkm.Radar.Radar.ViewModel
{
    public class RadarViewModel : ViewModelBase
    {
        private ScanLineViewModel ScanLine { get; }
        private Timer Timer { get; }

        public RadarViewModel()
        {
            Timer = new Timer(10);
            Timer.Elapsed += ScanLineMove;

            ScanLine = new ScanLineViewModel();
            Components = new ObservableCollection<IPositionalComponent>();

            InitializeComponents();

            Timer.Start();
        }

        public ObservableCollection<IPositionalComponent> Components { get; }

        private void ScanLineMove(object sender, ElapsedEventArgs e)
        {
            ScanLineDoStep();
            CheckTargetByScanLine();
        }

        private void InitializeComponents()
        {
            Components.Add(ScanLine);
            Components.Add(new TargetViewModel(120, 230, 10));
        }

        private void CheckTargetByScanLine()
        {
            //Статья с видами индикаторов рлс при постановке различных видов помех: https://studfiles.net/preview/1430298/page:8/
            // TODO: Обнаружение цели
            // TODO: Обнаружение ложных целей
            // TODO: Обнаружение помехи


        }

        private void ScanLineDoStep()
        {
            if (ScanLine.LineAzimuth > 360)
            {
                ScanLine.LineAzimuth = 0;
            }
            else
            {
                ScanLine.LineAzimuth += 1;
            }
        }
    }
}