using System;

namespace MedicalMystery.Models
{
    /// <summary>
    /// ErrorViewModel
    /// </summary>
    public class ErrorViewModel
    {
        /// <summary>
        /// ErrorViewModel RequestId
        /// </summary>
        public string RequestId { get; set; }

        /// <summary>
        /// ErrorViewModel test to see if RequestId is null or empty
        /// </summary>
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}