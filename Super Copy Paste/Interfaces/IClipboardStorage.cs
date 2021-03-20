namespace SuperCopyPaste
{
    public interface IClipboardStorage
    {
        T Read<T>();

        void Write<T>(T objectToWrite);
    }
}