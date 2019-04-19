using Sphaera.Web.Api.Helpers;
using Sphaera.Web.Api.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Sphaera.Web.Api.Models
{
    public enum Role
    {
        /// <summary>
        /// Гражданин; (только для этой роли доступен лк гражданина в people)
        /// </summary>
        [Identifier(Name = "Citizen")]
        [Display(ResourceType = typeof(Titles), Name = "Citizen")]
        Citizen,

        /// <summary>
        /// Диспетчер муниципальной службы или организации
        /// </summary>
        [Identifier(Name = "StaffDispatcher")]
        [Display(ResourceType = typeof(Titles), Name = "StaffDispatcher")]
        StaffDispatcher,

        /// <summary>
        /// Руководитель муниципальной службы или организации
        /// </summary>
        [Identifier(Name = "StaffSupervisor")]
        [Display(ResourceType = typeof(Titles), Name = "StaffSupervisor")]
        StaffSupervisor,

        /// <summary>
        /// Администратор службы или организации
        /// </summary>
        [Identifier(Name = "StaffAdmin")]
        [Display(ResourceType = typeof(Titles), Name = "StaffAdmin")]
        StaffAdmin,

        /// <summary>
        /// Администратор портала — обеспечивает работоспособность Портала
        /// </summary>
        [Identifier(Name = "PortalAdmin")]
        [Display(ResourceType = typeof(Titles), Name = "PortalAdmin")]
        PortalAdmin,

        /// <summary>
        /// Руководитель муниципального образования — выполняет функции только в рамках своего муниципального образования; — это руководитель муниципального образования (изредка даёт поручения); начальник управления ГОЧС (он управляет ЕДДС)
        /// </summary>
        [Identifier(Name = "ChiefMunicipal")]
        [Display(ResourceType = typeof(Titles), Name = "ChiefMunicipal")]
        ChiefMunicipal,

        /// <summary>
        /// Руководитель региона — выполняет функции в рамках всего региона
        /// </summary>
        [Identifier(Name = "ChiefRegion")]
        [Display(ResourceType = typeof(Titles), Name = "ChiefRegion")]
        ChiefRegion
    }
}
