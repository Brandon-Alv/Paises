using GalaSoft.MvvmLight.Command;
using Paises.Models;
using Paises.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Paises.ViewModels
{
    public class PaisesViewModel : BaseViewModel
    {
        #region Servicios
        private ApiService apiservice;

        #endregion

        #region Atributos
        private string filter;
        private bool isrefreshing;
        private ObservableCollection<Pais> paises;
        private ObservableCollection<Pais> paisesrefresh;
        #endregion

        #region Propiedades
        public string Filter
        {
            get { return this.filter; }
            set
            {
                SetValue(ref this.filter, value);
                this.Search();
            }
        }
        public bool IsRefreshing
        {
            get { return this.isrefreshing; }
            set { SetValue(ref this.isrefreshing, value); }
        }
        public ObservableCollection<Pais> Paises
        {
            get { return this.paises; }
            set { SetValue(ref this.paises, value); }
        }
        #endregion

        #region Constructor
        public PaisesViewModel()
        {
            this.apiservice = new ApiService();
            this.LoadPaises();
        }


        #endregion

        #region Comandos
        public ICommand SearchCommand
        {
            get
            {
                return new RelayCommand(Search);
            }
        }

        public ICommand RefreshCommand
        {
            get
            {
                return new RelayCommand(LoadPaises);
            }

        }


        #endregion

        #region Metodos
        private void Search()
        {
            if (string.IsNullOrEmpty(this.Filter))
            {
                this.Paises = this.paisesrefresh;
            }
            else
            {
                this.Paises = new ObservableCollection<Pais>(
                    this.paisesrefresh.Where(
                        l => l.Name.ToLower().Contains(this.Filter.ToLower()) ||
                             l.Capital.ToLower().Contains(this.Filter.ToLower())));
            }
        }

      
        
 

        private async void LoadPaises()
        {
            this.IsRefreshing = true;

            var connection = await this.apiservice.CheckConnection();

            if (!connection.IsSuccess)
            {
                this.IsRefreshing = false;
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    connection.Message,
                    "Aceptar");


                return;
            }


            var response = await this.apiservice.GetList<Pais>("https://restcountries.eu", "/rest", "/v2/all");

            if (!response.IsSuccess)
            {
                this.IsRefreshing = false;
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    response.Message,
                    "Aceptar");
                await Application.Current.MainPage.Navigation.PopAsync();
                return;
            }

            MainViewModel.GetInstance().ListaPaises = (List<Pais>)response.Result;
            this.Paises = new ObservableCollection<Pais>((List<Pais>)response.Result);
            this.paisesrefresh = new ObservableCollection<Pais>((List<Pais>)response.Result);

            this.IsRefreshing = false;



        }
        #endregion
    }
}
