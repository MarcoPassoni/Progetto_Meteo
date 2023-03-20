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

        public ModelViewDetails(Locations location)
        {
            Location = location;
        }
    }
}
