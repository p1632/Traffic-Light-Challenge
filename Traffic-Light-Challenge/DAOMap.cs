namespace Traffic_Light_Challenge
{
    public interface DAOMap
    {
        /// <summary>
        /// IF a map with map.ID of this object does not exist yet,
        /// create(save) new Map
        /// ELSE overwrite existing map
        /// </summary>
        /// <param name="map">Map object</param>
        void SaveMap(Map map);
        /// <summary>
        /// Returns a Map object if ID exists,
        /// otherwise NULL
        /// </summary>
        /// <param name="mapID">ID of the Map object</param>
        /// <returns></returns>
        Map LoadMap(uint mapID);
    }
}