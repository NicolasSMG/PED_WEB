using ChartJs.Blazor.BarChart;
using ChartJs.Blazor.Common;
using ChartJs.Blazor.Util;
using Microsoft.JSInterop;
using GrupoBIOS_PEDWEB.DT.DTOs;
using GrupoBIOS_PEDWEB.PWA.Helpers.Interfaces;
using GrupoBIOS_PEDWEB.PWA.NET6.Helpers;
using GrupoBIOS_PEDWEB.PWA.NET6.Model.Graficos.Interfaces;
using System.Reflection;
using System.Drawing;

namespace GrupoBIOS_PEDWEB.PWA.NET6.Model.Graficos
{
    public class DataSetGraficos_Model : IDataSetGraficos_Model
    {
        private readonly IConexionRest ConexionRest;
        private readonly ISettings Settings;
        private readonly ILogger<DataSetGraficos_Model> Logger;
        private Random rnd = new Random();
        public DataSetGraficos_Model(IConexionRest ConexionRest,
                                                ISettings Settings,
                                                ILogger<DataSetGraficos_Model> Logger)
        {
            this.ConexionRest = ConexionRest;
            this.Settings = Settings;
            this.Logger = Logger;
        }

        public async Task<List<IDataset>> GenerarDatasetLineaAnualAsync(DataSetsDTO DataSetsDTO)
        {
            return await Task.Run(() =>
            {
                List<IDataset> ListadoDataset = new();

                foreach (var item in DataSetsDTO.DataSetLineaAnual)
                {
                    ListadoDataset.Add(new BarDataset<double>(new double[] { item.Total })
                    {
                        Label = item.Linea,
                        BackgroundColor = ColorUtil.FromDrawingColor(Color.FromArgb(rnd.Next(256), rnd.Next(256), rnd.Next(256)))
                    });
                }
                return ListadoDataset;
            });
        }


        public async Task<List<IDataset>> GenerarDatasetLineaMensualAsync(DataSetsDTO DataSetsDTO)
        {
            return await Task.Run(() =>
            {
                List<IDataset> ListadoDataset = new();

                foreach (var item in DataSetsDTO.DataSetLineaMensual)
                {
                    ListadoDataset.Add(new BarDataset<double>(new double[] { item.Total })
                    {
                        Label = item.Linea,
                        BackgroundColor = ColorUtil.FromDrawingColor(Color.FromArgb(rnd.Next(256), rnd.Next(256), rnd.Next(256)))
                    });
                }

                return ListadoDataset;
            });
        }

        public async Task<List<IDataset>> GenerarDatasetNombreConductorAnualAsync(DataSetsDTO DataSetsDTO)
        {
            return await Task.Run(() =>
            {
                List<IDataset> ListadoDataset = new();

                foreach (var item in DataSetsDTO.DataSetNombreConductorAnual)
                {
                    ListadoDataset.Add(new BarDataset<double>(new double[] { item.Total })
                    {
                        Label = item.NombreConductor,
                        BackgroundColor = ColorUtil.FromDrawingColor(Color.FromArgb(rnd.Next(256), rnd.Next(256), rnd.Next(256)))
                    });
                }

                return ListadoDataset;
            });
        }


        public async Task<List<IDataset>> GenerarDatasetNombreConductorMensualAsync(DataSetsDTO DataSetsDTO)
        {
            return await Task.Run(() =>
            {
                List<IDataset> ListadoDataset = new();

                foreach (var item in DataSetsDTO.DataSetNombreConductorMensual)
                {
                    ListadoDataset.Add(new BarDataset<double>(new double[] { item.Total })
                    {
                        Label = item.NombreConductor,
                        BackgroundColor = ColorUtil.FromDrawingColor(Color.FromArgb(rnd.Next(256), rnd.Next(256), rnd.Next(256)))
                    });
                }

                return ListadoDataset;
            });
        }

        public async Task<List<IDataset>> GenerarDatasetNombrePlacaAnualAsync(DataSetsDTO DataSetsDTO)
        {
            return await Task.Run(() =>
            {
                List<IDataset> ListadoDataset = new();

                foreach (var item in DataSetsDTO.DataSetPlacaMensual)
                {
                    ListadoDataset.Add(new BarDataset<double>(new double[] { item.Total })
                    {
                        Label = item.PlacaVehiculo,
                        BackgroundColor = ColorUtil.FromDrawingColor(Color.FromArgb(rnd.Next(256), rnd.Next(256), rnd.Next(256)))
                    });
                }

                return ListadoDataset;
            });
        }

        public async Task<List<IDataset>> GenerarDatasetNombrePlacaMensualAsync(DataSetsDTO DataSetsDTO)
        {
            return await Task.Run(() =>
            {
                List<IDataset> ListadoDataset = new();

                foreach (var item in DataSetsDTO.DataSetPlacaAnual)
                {
                    ListadoDataset.Add(new BarDataset<double>(new double[] { item.Total })
                    {
                        Label = item.PlacaVehiculo,
                        BackgroundColor = ColorUtil.FromDrawingColor(Color.FromArgb(rnd.Next(256), rnd.Next(256), rnd.Next(256)))
                    });
                }

                return ListadoDataset;
            });
        }

        public async Task<List<IDataset>> GenerarDatasetVentasLineaAsync(VentaPorLineaDTO VentaPorLineaMensualDTO, VentaPorLineaDTO VentaPorLineaAnualDTO)
        {
            return await Task.Run(() =>
            {
                List<IDataset> ListadoDataset = new()
                {
                    new BarDataset<double>(new double[] { VentaPorLineaMensualDTO.Acuacultura.Total, VentaPorLineaAnualDTO.Acuacultura.Total })
                    {
                        Label = "Linea Acuacultura",
                        BackgroundColor = ColorUtil.FromDrawingColor(ChartColors.Red)
                    },
                    new BarDataset<double>(new double[] { VentaPorLineaMensualDTO.Avicultura.Total, VentaPorLineaAnualDTO.Avicultura.Total })
                    {
                        Label = "Linea Avicultura",
                        BackgroundColor = ColorUtil.FromDrawingColor(ChartColors.Yellow)
                    },
                    new BarDataset<double>(new double[] { VentaPorLineaMensualDTO.Equinos.Total, VentaPorLineaAnualDTO.Equinos.Total })
                    {
                        Label = "Linea Equinos",
                        BackgroundColor = ColorUtil.FromDrawingColor(ChartColors.Blue)
                    },
                    new BarDataset<double>(new double[] { VentaPorLineaMensualDTO.Ganaderia.Total, VentaPorLineaAnualDTO.Ganaderia.Total })
                    {
                        Label = "Linea Ganaderia",
                        BackgroundColor = ColorUtil.FromDrawingColor(ChartColors.Orange)
                    },
                    new BarDataset<double>(new double[] { VentaPorLineaMensualDTO.Mascotas.Total, VentaPorLineaAnualDTO.Mascotas.Total })
                    {
                        Label = "Linea Mascotas",
                        BackgroundColor = ColorUtil.FromDrawingColor(ChartColors.Green)
                    },
                    new BarDataset<double>(new double[] { VentaPorLineaMensualDTO.Otros.Total, VentaPorLineaAnualDTO.Otros.Total })
                    {
                        Label = "Linea Otros",
                        BackgroundColor = ColorUtil.FromDrawingColor(ChartColors.Purple)
                    },
                    new BarDataset<double>(new double[] { VentaPorLineaMensualDTO.Ovinos.Total, VentaPorLineaAnualDTO.Ovinos.Total })
                    {
                        Label = "Linea Ovinos",
                        BackgroundColor = ColorUtil.FromDrawingColor(ChartColors.Cyan)
                    },
                    new BarDataset<double>(new double[] { VentaPorLineaMensualDTO.Porcicultura.Total, VentaPorLineaAnualDTO.Porcicultura.Total })
                    {
                        Label = "Linea Porcicultura",
                        BackgroundColor = ColorUtil.FromDrawingColor(ChartColors.Grey)
                    },
                    new BarDataset<double>(new double[] { VentaPorLineaMensualDTO.Sales.Total, VentaPorLineaAnualDTO.Sales.Total })
                    {
                        Label = "Linea Sales",
                        BackgroundColor = ColorUtil.FromDrawingColor(ChartColors.Red)
                    },
                };

                return ListadoDataset;
            });
        }
        public async Task<DataSetsDTO> GetDataSetDTO(int CompaniaId)
        {
            try
            {
                var ApiUrl = await Settings.GetApiUrl();
                var httpResponse = await ConexionRest.Get<DataSetsDTO>($"{ApiUrl}/DataSets/GenerarDataSets/{CompaniaId}");
                if (!httpResponse.Error)
                {
                    return httpResponse.Response;
                }
                return null;
            }
            catch (Exception ex)
            {
                Logger.LogError($"Clase: {GetType().Name}, Metodo: {MethodBase.GetCurrentMethod().DeclaringType.Name}, Tipo: {ex.GetType()}, Error: {ex.Message}");
            }
            return null;
        }
        public async Task<DataSetsDTO> GetDataSetClienteDTO(int Nit)
        {
            try
            {
                var ApiUrl = await Settings.GetApiUrl();
                var httpResponse = await ConexionRest.Get<DataSetsDTO>($"{ApiUrl}/DataSets/GenerarDataSetsCliente/{Nit}");
                if (!httpResponse.Error)
                {
                    return httpResponse.Response;
                }
                return null;
            }
            catch (Exception ex)
            {
                Logger.LogError($"Clase: {GetType().Name}, Metodo: {MethodBase.GetCurrentMethod().DeclaringType.Name}, Tipo: {ex.GetType()}, Error: {ex.Message}");
            }
            return null;
        }

        public async Task<VentaPorLineaDTO> GetDataSetVentasLineaAnualDTO(int CompaniaId)
        {
            try
            {
                var ApiUrl = await Settings.GetApiUrl();
                var httpResponse = await ConexionRest.Get<VentaPorLineaDTO>($"{ApiUrl}/DataSets/GenerarDataSetsVentaPorLineaAnual/{CompaniaId}");
                if (!httpResponse.Error)
                {
                    return httpResponse.Response;
                }
                return null;
            }
            catch (Exception ex)
            {
                Logger.LogError($"Clase: {GetType().Name}, Metodo: {MethodBase.GetCurrentMethod().DeclaringType.Name}, Tipo: {ex.GetType()}, Error: {ex.Message}");
            }
            return null;
        }
        public async Task<VentaPorLineaDTO> GetDataSetVentasLineaAnualClienteDTO(int Nit)
        {
            try
            {
                var ApiUrl = await Settings.GetApiUrl();
                var httpResponse = await ConexionRest.Get<VentaPorLineaDTO>($"{ApiUrl}/DataSets/GenerarDataSetsVentaPorLineaAnualCliente/{Nit}");
                if (!httpResponse.Error)
                {
                    return httpResponse.Response;
                }
                return null;
            }
            catch (Exception ex)
            {
                Logger.LogError($"Clase: {GetType().Name}, Metodo: {MethodBase.GetCurrentMethod().DeclaringType.Name}, Tipo: {ex.GetType()}, Error: {ex.Message}");
            }
            return null;
        }
        public async Task<VentaPorLineaDTO> GetDataSetVentasLineaMensualDTO(int CompaniaId)
        {
            try
            {
                var ApiUrl = await Settings.GetApiUrl();
                var httpResponse = await ConexionRest.Get<VentaPorLineaDTO>($"{ApiUrl}/DataSets/GenerarDataSetsVentaPorLineaMensual/{CompaniaId}");
                if (!httpResponse.Error)
                {
                    return httpResponse.Response;
                }
                return null;
            }
            catch (Exception ex)
            {
                Logger.LogError($"Clase: {GetType().Name}, Metodo: {MethodBase.GetCurrentMethod().DeclaringType.Name}, Tipo: {ex.GetType()}, Error: {ex.Message}");
            }
            return null;
        }
        public async Task<VentaPorLineaDTO> GetDataSetVentasLineaMensualClienteDTO(int Nit)
        {
            try
            {
                var ApiUrl = await Settings.GetApiUrl();
                var httpResponse = await ConexionRest.Get<VentaPorLineaDTO>($"{ApiUrl}/DataSets/GenerarDataSetsVentaPorLineaMensualCliente/{Nit}");
                if (!httpResponse.Error)
                {
                    return httpResponse.Response;
                }
                return null;
            }
            catch (Exception ex)
            {
                Logger.LogError($"Clase: {GetType().Name}, Metodo: {MethodBase.GetCurrentMethod().DeclaringType.Name}, Tipo: {ex.GetType()}, Error: {ex.Message}");
            }
            return null;
        }
    }
}
