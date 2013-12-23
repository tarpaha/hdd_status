namespace hdd_status
{
    interface IDataSender
    {
        void Create();
        void Send(float value);
        void Destroy();
    }
}
