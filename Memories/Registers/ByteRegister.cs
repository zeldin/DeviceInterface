﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LabNation.DeviceInterface.Memories
{
#if DEBUG
    public
#else
    internal
#endif    
    class ByteRegister : MemoryRegister
    {
        private byte internalValue
        {
            get { return (byte)_internalValue; }
            set { _internalValue = value; }
        }

        internal ByteRegister(DeviceMemory memory, uint address, string name) : base(memory, address, name) 
        {
            this.internalValue = 0;
        }

        public override MemoryRegister Set(object value)
        {
            byte castValue;
            if(!value.GetType().Equals(typeof(byte))) 
            {
                try
                {
                    castValue = (byte)((int)value & 0xFF);
                    if ((int)value != (int)castValue)
                        throw new Exception("Cast to byte resulted in loss of information");
                }
                catch (InvalidCastException)
                {
                    throw new Exception("Cannot set ByteRegister with that kind of type (" + value.GetType().Name + ")");
                }
            }
            else
                castValue = (byte)value;
            this.internalValue = castValue;
            CallValueChangedCallbacks();
            return this;
        }

        public override object Get() { return this.internalValue; }

        public byte GetByte() { return this.internalValue; }

        public new ByteRegister Read() { return (ByteRegister)base.Read(); }

        public override int MaxValue { get { return 255; } }
    }
}
