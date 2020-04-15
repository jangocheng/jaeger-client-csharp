/**
 * Autogenerated by Thrift Compiler (0.13.0)
 *
 * DO NOT EDIT UNLESS YOU ARE SURE THAT YOU KNOW WHAT YOU ARE DOING
 *  @generated
 */

using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Thrift.Protocol;
using Thrift.Protocol.Entities;
using Thrift.Protocol.Utilities;


namespace Jaeger.Thrift.Crossdock
{

  public partial class ObservedSpan : TBase
  {

    public string TraceId { get; set; }

    public bool Sampled { get; set; }

    public string Baggage { get; set; }

    public ObservedSpan()
    {
    }

    public ObservedSpan(string traceId, bool sampled, string baggage) : this()
    {
      this.TraceId = traceId;
      this.Sampled = sampled;
      this.Baggage = baggage;
    }

    public async Task ReadAsync(TProtocol iprot, CancellationToken cancellationToken)
    {
      iprot.IncrementRecursionDepth();
      try
      {
        bool isset_traceId = false;
        bool isset_sampled = false;
        bool isset_baggage = false;
        TField field;
        await iprot.ReadStructBeginAsync(cancellationToken);
        while (true)
        {
          field = await iprot.ReadFieldBeginAsync(cancellationToken);
          if (field.Type == TType.Stop)
          {
            break;
          }

          switch (field.ID)
          {
            case 1:
              if (field.Type == TType.String)
              {
                TraceId = await iprot.ReadStringAsync(cancellationToken);
                isset_traceId = true;
              }
              else
              {
                await TProtocolUtil.SkipAsync(iprot, field.Type, cancellationToken);
              }
              break;
            case 2:
              if (field.Type == TType.Bool)
              {
                Sampled = await iprot.ReadBoolAsync(cancellationToken);
                isset_sampled = true;
              }
              else
              {
                await TProtocolUtil.SkipAsync(iprot, field.Type, cancellationToken);
              }
              break;
            case 3:
              if (field.Type == TType.String)
              {
                Baggage = await iprot.ReadStringAsync(cancellationToken);
                isset_baggage = true;
              }
              else
              {
                await TProtocolUtil.SkipAsync(iprot, field.Type, cancellationToken);
              }
              break;
            default: 
              await TProtocolUtil.SkipAsync(iprot, field.Type, cancellationToken);
              break;
          }

          await iprot.ReadFieldEndAsync(cancellationToken);
        }

        await iprot.ReadStructEndAsync(cancellationToken);
        if (!isset_traceId)
        {
          throw new TProtocolException(TProtocolException.INVALID_DATA);
        }
        if (!isset_sampled)
        {
          throw new TProtocolException(TProtocolException.INVALID_DATA);
        }
        if (!isset_baggage)
        {
          throw new TProtocolException(TProtocolException.INVALID_DATA);
        }
      }
      finally
      {
        iprot.DecrementRecursionDepth();
      }
    }

    public async Task WriteAsync(TProtocol oprot, CancellationToken cancellationToken)
    {
      oprot.IncrementRecursionDepth();
      try
      {
        var struc = new TStruct("ObservedSpan");
        await oprot.WriteStructBeginAsync(struc, cancellationToken);
        var field = new TField();
        field.Name = "traceId";
        field.Type = TType.String;
        field.ID = 1;
        await oprot.WriteFieldBeginAsync(field, cancellationToken);
        await oprot.WriteStringAsync(TraceId, cancellationToken);
        await oprot.WriteFieldEndAsync(cancellationToken);
        field.Name = "sampled";
        field.Type = TType.Bool;
        field.ID = 2;
        await oprot.WriteFieldBeginAsync(field, cancellationToken);
        await oprot.WriteBoolAsync(Sampled, cancellationToken);
        await oprot.WriteFieldEndAsync(cancellationToken);
        field.Name = "baggage";
        field.Type = TType.String;
        field.ID = 3;
        await oprot.WriteFieldBeginAsync(field, cancellationToken);
        await oprot.WriteStringAsync(Baggage, cancellationToken);
        await oprot.WriteFieldEndAsync(cancellationToken);
        await oprot.WriteFieldStopAsync(cancellationToken);
        await oprot.WriteStructEndAsync(cancellationToken);
      }
      finally
      {
        oprot.DecrementRecursionDepth();
      }
    }

    public override bool Equals(object that)
    {
      var other = that as ObservedSpan;
      if (other == null) return false;
      if (ReferenceEquals(this, other)) return true;
      return System.Object.Equals(TraceId, other.TraceId)
        && System.Object.Equals(Sampled, other.Sampled)
        && System.Object.Equals(Baggage, other.Baggage);
    }

    public override int GetHashCode() {
      int hashcode = 157;
      unchecked {
        hashcode = (hashcode * 397) + TraceId.GetHashCode();
        hashcode = (hashcode * 397) + Sampled.GetHashCode();
        hashcode = (hashcode * 397) + Baggage.GetHashCode();
      }
      return hashcode;
    }

    public override string ToString()
    {
      var sb = new StringBuilder("ObservedSpan(");
      sb.Append(", TraceId: ");
      sb.Append(TraceId);
      sb.Append(", Sampled: ");
      sb.Append(Sampled);
      sb.Append(", Baggage: ");
      sb.Append(Baggage);
      sb.Append(")");
      return sb.ToString();
    }
  }

}