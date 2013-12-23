namespace hdd_status
{
    interface IDataCollector
    {
        void Create();
        float Collect();
        void Destroy();
    }
}
