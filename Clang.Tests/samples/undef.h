//
// Hephestos 2 24V heated bed upgrade kit.
// https://store.bq.com/en/heated-bed-kit-hephestos2
//
//#define HEPHESTOS2_HEATED_BED_KIT
#if ENABLED(HEPHESTOS2_HEATED_BED_KIT)
    #undef TEMP_SENSOR_BED
    #define TEMP_SENSOR_BED 70
    #define HEATER_BED_INVERTING true
#endif