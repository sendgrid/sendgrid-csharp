using System;

namespace SendGrid.Tests.Helpers.Mail
{

    public class ChunkedReadableStream : NonReadableStream
    {
        public ChunkedReadableStream(int length, int steps)
        {
            Length = length;
            PositionStepSize = length / steps;
        }

        public int PositionStepSize { get; }

        public int CurrentStep => (int)(Position / PositionStepSize);

        public override bool CanRead => true;

        public override long Length { get; }

        public override long Position { get; set; }

        public override int Read(byte[] buffer, int offset, int count)
        {
            // limit the "bytes read" by the following:
            // - step size
            // - remaining step size
            // - the given count
            // - the remaining length of the buffer.
            var bytesRead = Math.Min(
                                Math.Min(PositionStepSize, Length - Position),
                                Math.Min(count, buffer.Length - offset));

            // increment the current position of the stream
            Position += bytesRead;

            return (int)bytesRead;
        }
    }
}
