using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace av.mmn.gui
{
    public struct ChangeableKeyValuePair<TKey, TValue>
    {
        public TKey Key { get; }
        public TValue Value { get; set; }

        public ChangeableKeyValuePair(TKey key, TValue value)
        {
            Key = key;
            Value = value;
        }
    }
}
