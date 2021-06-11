﻿using System.Collections.Generic;
using JOIEnergy.Domain;

namespace JOIEnergy.Services.Interfaces
{
    public interface IMeterReadingService
    {
        List<ElectricityReading> GetReadings(string smartMeterId);
        void StoreReadings(string smartMeterId, List<ElectricityReading> electricityReadings);
    }
}