namespace ServerApplication.Features.Items
{
    public class ItemIdPooling:IDependency
    {
        #region Fields

        private object _lockObj = new object();

        private uint _id;

        #endregion

        public uint NewId()
        {
            lock (_lockObj)
            {
                return ++_id;
            }
        }
    }
}
