using ChartJs.Blazor.Common;
using GrupoBIOS_PEDWEB.BM.Graficos;
using GrupoBIOS_PEDWEB.DM;
using GrupoBIOS_PEDWEB.DM.DataBase.Interfaces;
using GrupoBIOS_PEDWEB.DT.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GrupoBIOS_PEDWEB.BM.Graficos
{
    public class BMDataSets : IBMDataSets
    {
        private readonly IDMDTOs DMDTOs;
        private readonly ILogger<BMDataSets> Logger;
        public BMDataSets(IDMDTOs DMDTOs
            , ILogger<BMDataSets> Logger)
        {
            this.Logger = Logger;
            this.DMDTOs = DMDTOs;
        }

        public async Task<ActionResult<DataSetsDTO>> GenerarDatasetGraficoTop10PlacaVehiculoMensualAsync(int CompaniaId)
        {
            try
            {
                var Formulario = await DMDTOs.ConstruirDataSetsDTO(CompaniaId);
                if (Formulario != null)
                {
                    return Formulario;
                }
                return null;
            }
            catch (Exception ex)
            {
                Logger.LogError($"Ha ocurrido un {GetType().Name}/{MethodBase.GetCurrentMethod().DeclaringType.Name}: {ex.Message}");
                return null;
            }
        }
        public async Task<ActionResult<DataSetsDTO>> GenerarDatasetGraficoTop10PlacaVehiculoMensualClienteAsync(int Nit)
        {
            try
            {
                var Formulario = await DMDTOs.ConstruirDataSetsClienteDTO(Nit);
                if (Formulario != null)
                {
                    return Formulario;
                }
                return null;
            }
            catch (Exception ex)
            {
                Logger.LogError($"Ha ocurrido un {GetType().Name}/{MethodBase.GetCurrentMethod().DeclaringType.Name}: {ex.Message}");
                return null;
            }
        }

        public async Task<ActionResult<VentaPorLineaDTO>> GenerarDatasetGraficoVentasPorLineaAnualAsync(int CompaniaId)
        {
            try
            {
                var Formulario = await DMDTOs.VentasPorLineaAnualDTO(CompaniaId);
                if (Formulario != null)
                {
                    return Formulario;
                }
                return null;
            }
            catch (Exception ex)
            {
                Logger.LogError($"Ha ocurrido un {GetType().Name}/{MethodBase.GetCurrentMethod().DeclaringType.Name}: {ex.Message}");
                return null;
            }
        }

        public async Task<ActionResult<VentaPorLineaDTO>> GenerarDatasetGraficoVentasPorLineaAnualClienteAsync(int Nit)
        {
            try
            {
                var Formulario = await DMDTOs.VentasPorLineaAnualClienteDTO(Nit);
                if (Formulario != null)
                {
                    return Formulario;
                }
                return null;
            }
            catch (Exception ex)
            {
                Logger.LogError($"Ha ocurrido un {GetType().Name}/{MethodBase.GetCurrentMethod().DeclaringType.Name}: {ex.Message}");
                return null;
            }
        }

        public async Task<ActionResult<VentaPorLineaDTO>> GenerarDatasetGraficoVentasPorLineaMensualAsync(int CompaniaId)
        {
            try
            {
                var Formulario = await DMDTOs.VentasPorLineaMensualDTO(CompaniaId);
                if (Formulario != null)
                {
                    return Formulario;
                }
                return null;
            }
            catch (Exception ex)
            {
                Logger.LogError($"Ha ocurrido un {GetType().Name}/{MethodBase.GetCurrentMethod().DeclaringType.Name}: {ex.Message}");
                return null;
            }
        }

        public async Task<ActionResult<VentaPorLineaDTO>> GenerarDatasetGraficoVentasPorLineaMensualClienteAsync(int Nit)
        {
            try
            {
                var Formulario = await DMDTOs.VentasPorLineaMensualClienteDTO(Nit);
                if (Formulario != null)
                {
                    return Formulario;
                }
                return null;
            }
            catch (Exception ex)
            {
                Logger.LogError($"Ha ocurrido un {GetType().Name}/{MethodBase.GetCurrentMethod().DeclaringType.Name}: {ex.Message}");
                return null;
            }
        }
    }
}
