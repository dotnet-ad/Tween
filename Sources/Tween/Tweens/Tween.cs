namespace Tweening
{
	using System;
	using System.Linq;
	using System.Collections.Generic;
	using System.Linq.Expressions;
	using System.Reflection;
	using Tweening.Tweeners;

	public class Tween : Timer
	{
		public Tween(object source, double duration, dynamic toValues, dynamic fromValues = null, Func<float, float> ease = null) : base(duration)
		{
			this.ease = ease ?? Ease.QuadInOut;
			this.Source = source;
			this.toValues = toValues;
			this.fromValues = fromValues;
			this.target = source;
		}

		#region Fields

		private object target;

		private dynamic toValues, fromValues;

		private Func<float, float> ease;

		private Action<double> update;

		#endregion

		#region Properties

		public object Source { get; }

		public override double Time
		{
			get => base.Time;
			set
			{
				if(value != this.Time)
				{
					this.update = value == 0 ? null : this.update ?? CreateUpdate(target, toValues, fromValues);
				}

				base.Time = value;
				this.update?.Invoke(ease(this.Progress));
			}
		}

		#endregion

		#region Internal methods

		private static IDictionary<string,object> GetValues(dynamic d)
		{
			if (d == null) return new Dictionary<string, object>();
			IEnumerable<PropertyInfo> properties = d.GetType().GetProperties();
			return properties.ToDictionary(x => x.Name, x => x.GetValue(d));;
		}

		private static Type GetSubstractableType(Type t)
		{
			if (t == typeof(byte))
				return typeof(int);

			return t;
		}

		private static Action<double> CreateUpdate(object target, dynamic toValues, dynamic fromValues)
		{
			IDictionary<string, object> To = GetValues(toValues);
			IDictionary<string, object> From = GetValues(fromValues);

			var timeParameter = Expression.Parameter(typeof(double), "time");

			var instructions = new List<Expression>();

			var source = Expression.Constant(target);

			foreach (var p in To)
			{
				var info = target.GetType().GetRuntimeProperty(p.Key);
				var property = Expression.Property(source,info);
				var valuetype = GetSubstractableType(p.Value.GetType());

				Expression fromValue;
				if (From.TryGetValue(p.Key, out object fromRaw))
				{
					fromValue = Expression.Convert(Expression.Constant(fromRaw), valuetype);
				}
				else
				{
					// Getting initial value if not precised
					var frominfo = target.GetType().GetRuntimeProperty(p.Key);
					fromValue = Expression.Convert( Expression.Constant(frominfo.GetValue(target)), valuetype);
				}

				var toValue = Expression.Convert(Expression.Constant(p.Value),valuetype);
				var range = Expression.Subtract(toValue, fromValue);
				var amount = Expression.Multiply(Expression.Convert(range, typeof(double)), timeParameter);
				var value = Expression.Add(fromValue, Expression.Convert(amount, valuetype));

				var assignment = Expression.Assign(property, Expression.Convert(value, info.PropertyType));
				instructions.Add(assignment);
			}

			var body = Expression.Block(instructions);

			return Expression.Lambda<Action<double>>(body, timeParameter).Compile();
		}


		#endregion
	}
}
