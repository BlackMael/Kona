using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHibernate.UserTypes;
using Kona.Model.Supply.Inventory;
using NHibernate.SqlTypes;
using System.Data;

namespace Kona.Infrastructure {
    public enum InventoryStatus {
        InStock = 1,
        BackOrder = 2,
        PreOrder = 3,
        SpecialOrder = 4,
        Discontinued = 5,
        CurrentlyUnavailable = 6
    }
    [Serializable]
    public class InventoryStateType:IUserType {


        public object Assemble(object cached, object owner) {
            return InventoryState.Create((InventoryStatus)cached);
        }
        public object Disassemble(object value) {
            return ((InventoryState)value).Status;
        }
        public object DeepCopy(object value) {
            return InventoryState.Create(((InventoryState)value).Status);
        }

        public int GetHashCode(object x) {
            return ((InventoryState)x).Status.GetHashCode();
        }

        public bool IsMutable {
            get { return false; }
        }

        public object NullSafeGet(System.Data.IDataReader rs, string[] names, object owner) {
            return InventoryState.Create((InventoryStatus)rs[names[0]]);
        }

        public void NullSafeSet(System.Data.IDbCommand cmd, object value, int index) {
            ((IDataParameter)cmd.Parameters[index]).Value = ((InventoryState)value).Status;
        }

        public object Replace(object original, object target, object owner) {
            return DeepCopy(original);
        }

        public Type ReturnedType {
            get { return typeof(InventoryState); }
        }

        public NHibernate.SqlTypes.SqlType[] SqlTypes {
            get { return new SqlType[] { new SqlType(DbType.Int32) }; }
        }
        public bool Equals(object x, object y) {
            return ((InventoryState)x).Status.Equals(((InventoryState)y).Status);
        }
    }
}
