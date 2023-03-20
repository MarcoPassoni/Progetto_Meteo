using CommunityToolkit.Mvvm.ComponentModel;
using Progetto.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Progetto.ModelView
{
    public partial class ModelViewDetails : ObservableObject
    {
        [ObservableProperty]
        Locations location;

        [ObservableProperty]
        public string data1;

        [ObservableProperty]
        public string tempMinima;

        [ObservableProperty]
        public string tempMaxima;

        public ModelViewDetails(Locations location, string data1, string tempMin, string tempMax)
        {
            Location = location;
            Data1 = data1;
            TempMinima = tempMin;
            TempMaxima = tempMax;
        }
    }
}
