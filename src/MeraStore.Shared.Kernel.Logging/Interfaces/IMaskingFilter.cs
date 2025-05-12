namespace MeraStore.Shared.Kernel.Logging.Interfaces;

public interface IMaskingFilter
{
  byte[] MaskRequestPayload(byte[] payload, string contentType = "application/json");
  byte[] MaskResponsePayload(byte[] payload);
}