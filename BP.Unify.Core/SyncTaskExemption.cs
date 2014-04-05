using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BP.Unify.Core
{
    class SyncTaskExemption
    {
		private ExemptionEntity _entity;
		private ExemptionOperator _operator;
		private string _value;

		public SyncTaskExemption()
		{

		}

		public SyncTaskExemption(ExemptionEntity entity, ExemptionOperator @operator, string value)
		{
			this.Entity = entity;
			this.Operator = @operator;
			this.Value = value;
		}

		#region PROPERTIES

		public ExemptionEntity Entity
		{
			get { return _entity; }
			set { _entity = value; }
		}

		public ExemptionOperator @Operator
		{
			get { return _operator; }
			set { _operator = value; }
		}

		public string Value
		{
			get { return _value; }
			set { _value = value; }
		}

		#endregion

		#region METHODS

		public SyncTaskExemption Clone()
		{
			SyncTaskExemption item = new SyncTaskExemption();
			item.Entity = this.Entity;
			item.Operator = this.Operator;
			item.Value = this.Value;
			return item;
		}

		public override string ToString()
		{
			if (this.Entity != ExemptionEntity.FileSize)
			{
				return Common.FormattedExemptionEntities[this.Entity] + " " + Common.FormattedExemptionOperators[this.Operator] + " " + this.Value;
			}
			else
			{
				return Common.FormattedExemptionEntities[this.Entity] + " " + Common.FormattedExemptionOperators[this.Operator] + " " + Int32.Parse(this.Value).ToString("N0") + " KB";
			}
		}

		#endregion
	}
}
