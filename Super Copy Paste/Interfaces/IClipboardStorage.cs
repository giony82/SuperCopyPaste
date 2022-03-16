namespace SuperCopyPaste.Interfaces
{
    public interface IClipboardStorage
    {
        T Read<T>() where T:new();

        void Write<T>(T objectToWrite);
    }
}