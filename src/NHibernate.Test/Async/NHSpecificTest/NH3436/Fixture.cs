﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by AsyncGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------


using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using NHibernate.Cfg.MappingSchema;
using NHibernate.Linq;
using NHibernate.Mapping.ByCode;
using NUnit.Framework;

namespace NHibernate.Test.NHSpecificTest.NH3436
{
	using System.Threading.Tasks;
	[TestFixture]
	public class FixtureAsync : TestCaseMappingByCode
	{
		protected override HbmMapping GetMappings()
		{
			var mapper = new ModelMapper();
			mapper.Class<TestEntity>(rc =>
			{
				rc.Table("TestEntity");
				rc.Id(x => x.Id, m => m.Generator(Generators.GuidComb));
			});
			return mapper.CompileMappingForAllExplicitlyAddedEntities();
		}

		protected override void OnSetUp()
		{
			using (var session = OpenSession())
			using (var transaction = session.BeginTransaction())
			{
				var e1 = new TestEntity();
				session.Save(e1);

				var e2 = new TestEntity();
				session.Save(e2);

				session.Flush();
				transaction.Commit();
			}
		}

		protected override void OnTearDown()
		{
			using (var session = OpenSession())
			using (var transaction = session.BeginTransaction())
			{
				session.Delete("from System.Object");

				session.Flush();
				transaction.Commit();
			}
		}
		
		[Test]
		public Task TestQueryWithContainsAsync()
		{
			try
			{
				var ids = new List<Guid>
			{
				Guid.NewGuid(),
				Guid.NewGuid(),
			};
				return RunAsync(ids);
			}
			catch (Exception ex)
			{
				return Task.FromException<object>(ex);
			}
		}

		private async Task RunAsync(ICollection<Guid> ids, CancellationToken cancellationToken = default(CancellationToken))
		{
			using (var session = Sfi.OpenSession())
			using (session.BeginTransaction())
			{
				var result = await (session.Query<TestEntity>()
					.Where(entity => ids.Contains(entity.Id))
					.ToListAsync(cancellationToken));

				Assert.That(result.Count, Is.EqualTo(0));
			}
		}

		public class TestEntity
		{
			public virtual Guid Id { get; set; }
		}
	}
}