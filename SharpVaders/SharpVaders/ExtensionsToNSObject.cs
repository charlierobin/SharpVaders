using System;
using Foundation;

namespace SharpVaders
{
    public static class ExtensionsToNSObject
    {
        // https://social.msdn.microsoft.com/Forums/en-US/1664151f-0739-4f4a-b9bc-56681e38db70/trying-to-track-down-cause-of-quotwarning-observer-object-was-not-disposed-manually-with?forum=xamarinios

        public static void SetObservableProperty(this NSObject obj, string key, Action setter)
        {
            // https://social.msdn.microsoft.com/Forums/en-US/0a7987e5-2e06-4e36-b5bc-a0151420717b/keyvalue-observing?forum=xamarinios

            //obj.WillChangeValue(key);

            //setter();

            //obj.DidChangeValue(key);
        }
    }
}

