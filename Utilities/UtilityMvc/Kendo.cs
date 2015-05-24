using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Kendo.Mvc;
using Kendo.Mvc.UI;

namespace UtilityMvc
{
    public static class SelectListHelper
    {
        public static SelectList EnumToSelectList( Type enumType )
        {
            var list = Enum
                .GetValues(enumType)
                .Cast<int>()
                .Select(i => new SelectListItem
                {
                    Value = i.ToString(),
                    Text = Enum.GetName(enumType, i),
                }
                );

            return new SelectList(list,"Value", "Text");
        }
    }
    public class CustomDataSourceRequestAttribute : DataSourceRequestAttribute
    {
        public override IModelBinder GetBinder()
        {
            return new CustomDataSourceRequestModelBinder();
        }
    }

    public class CustomDataSourceRequestModelBinder : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            // Get an instance of the original kendo model binder and call the binding method
            var baseBinder = new DataSourceRequestModelBinder();
            var request = (DataSourceRequest)baseBinder.BindModel(controllerContext, bindingContext);

            if (request.Filters != null && request.Filters.Count > 0)
            {
                var transformedFilters = request.Filters.Select(TransformFilterDescriptors).ToList();
                request.Filters = transformedFilters;
            }
            if (request.Groups != null && request.Groups.Count > 0)
            {
                var groups = request.Groups.Select(TransformGroupDescriptors).ToList();
                request.Groups = groups;
            }

            return request;
        }

        private GroupDescriptor TransformGroupDescriptors(GroupDescriptor group)
        {
            var groupDescriptor = group;
            return groupDescriptor;
        }

        private IFilterDescriptor TransformFilterDescriptors(IFilterDescriptor filter)
        {
            if (filter is CompositeFilterDescriptor)
            {
                var compositeFilterDescriptor = filter as CompositeFilterDescriptor;
                var transformedCompositeFilterDescriptor = new CompositeFilterDescriptor { LogicalOperator = compositeFilterDescriptor.LogicalOperator };
                foreach (var filterDescriptor in compositeFilterDescriptor.FilterDescriptors)
                {
                    transformedCompositeFilterDescriptor.FilterDescriptors.Add(TransformFilterDescriptors(filterDescriptor));
                }
                return transformedCompositeFilterDescriptor;
            }
            if (filter is FilterDescriptor)
            {
                var filterDescriptor = filter as FilterDescriptor;
                if (filterDescriptor.Value is DateTime)
                {
                    var value = (DateTime)filterDescriptor.Value;
                    switch (filterDescriptor.Operator)
                    {
                        case FilterOperator.IsEqualTo:
                            //convert the "is equal to <date><time>" filter to a "is greater than or equal to <date> 00:00:00" AND "is less than or equal to <date> 23:59:59"
                            var isEqualCompositeFilterDescriptor = new CompositeFilterDescriptor { LogicalOperator = FilterCompositionLogicalOperator.And };
                            isEqualCompositeFilterDescriptor.FilterDescriptors.Add(new FilterDescriptor(filterDescriptor.Member,
                                FilterOperator.IsGreaterThanOrEqualTo, new DateTime(value.Year, value.Month, value.Day, 0, 0, 0)));
                            isEqualCompositeFilterDescriptor.FilterDescriptors.Add(new FilterDescriptor(filterDescriptor.Member,
                                FilterOperator.IsLessThanOrEqualTo, new DateTime(value.Year, value.Month, value.Day, 23, 59, 59)));
                            return isEqualCompositeFilterDescriptor;

                        case FilterOperator.IsNotEqualTo:
                            //convert the "is not equal to <date><time>" filter to a "is less than <date> 00:00:00" OR "is greater than <date> 23:59:59"
                            var notEqualCompositeFilterDescriptor = new CompositeFilterDescriptor { LogicalOperator = FilterCompositionLogicalOperator.Or };
                            notEqualCompositeFilterDescriptor.FilterDescriptors.Add(new FilterDescriptor(filterDescriptor.Member,
                                FilterOperator.IsLessThan, new DateTime(value.Year, value.Month, value.Day, 0, 0, 0)));
                            notEqualCompositeFilterDescriptor.FilterDescriptors.Add(new FilterDescriptor(filterDescriptor.Member,
                                FilterOperator.IsGreaterThan, new DateTime(value.Year, value.Month, value.Day, 23, 59, 59)));
                            return notEqualCompositeFilterDescriptor;

                        case FilterOperator.IsGreaterThanOrEqualTo:
                            //convert the "is greater than or equal to <date><time>" filter to a "is greater than or equal to <date> 00:00:00"
                            filterDescriptor.Value = new DateTime(value.Year, value.Month, value.Day, 0, 0, 0);
                            return filterDescriptor;

                        case FilterOperator.IsGreaterThan:
                            //convert the "is greater than <date><time>" filter to a "is greater than <date> 23:59:59"
                            filterDescriptor.Value = new DateTime(value.Year, value.Month, value.Day, 23, 59, 59);
                            return filterDescriptor;

                        case FilterOperator.IsLessThanOrEqualTo:
                            //convert the "is less than or equal to <date><time>" filter to a "is less than or equal to <date> 23:59:59"
                            filterDescriptor.Value = new DateTime(value.Year, value.Month, value.Day, 23, 59, 59);
                            return filterDescriptor;

                        case FilterOperator.IsLessThan:
                            //convert the "is less than <date><time>" filter to a "is less than <date> 00:00:00"
                            filterDescriptor.Value = new DateTime(value.Year, value.Month, value.Day, 0, 0, 0);
                            return filterDescriptor;

                        default:
                            throw new Exception(string.Format("Filter operator '{0}' is not supported for DateTime member '{1}'", filterDescriptor.Operator, filterDescriptor.Member));
                    }
                }
            }
            return filter;
        }


    }


    /// <summary>
    /// Represents a filter expression of Kendo DataSource.
    /// </summary>
    [DataContract]
    public class Filter
    {
        /// <summary>
        /// Gets or sets the name of the sorted field (property). Set to <c>null</c> if the <c>Filters</c> property is set.
        /// </summary>
        [DataMember(Name = "field")]
        public string Field { get; set; }

        /// <summary>
        /// Gets or sets the filtering operator. Set to <c>null</c> if the <c>Filters</c> property is set.
        /// </summary>
        [DataMember(Name = "operator")]
        public string Operator { get; set; }

        /// <summary>
        /// Gets or sets the filtering value. Set to <c>null</c> if the <c>Filters</c> property is set.
        /// </summary>
        [DataMember(Name = "value")]
        public object Value { get; set; }

        /// <summary>
        /// Gets or sets the filtering logic. Can be set to "or" or "and". Set to <c>null</c> unless <c>Filters</c> is set.
        /// </summary>
        [DataMember(Name = "logic")]
        public string Logic { get; set; }

        /// <summary>
        /// Gets or sets the child filter expressions. Set to <c>null</c> if there are no child expressions.
        /// </summary>
        [DataMember(Name = "filters")]
        public IEnumerable<Filter> Filters { get; set; }

        /// <summary>
        /// Mapping of Kendo DataSource filtering operators to Dynamic Linq
        /// </summary>
        private static readonly IDictionary<string, string> operators = new Dictionary<string, string>
        {
            {"eq", "="},
            {"neq", "!="},
            {"lt", "<"},
            {"lte", "<="},
            {"gt", ">"},
            {"gte", ">="},
            {"startswith", "StartsWith"},
            {"endswith", "EndsWith"},
            {"contains", "Contains"},
            {"doesnotcontain", "Contains"}
        };

        /// <summary>
        /// Get a flattened list of all child filter expressions.
        /// </summary>
        public IList<Filter> All()
        {
            var filters = new List<Filter>();

            Collect(filters);

            return filters;
        }

        private void Collect(IList<Filter> filters)
        {
            if (Filters != null && Filters.Any())
            {
                foreach (Filter filter in Filters)
                {
                    filters.Add(filter);

                    filter.Collect(filters);
                }
            }
            else
            {
                filters.Add(this);
            }
        }

        /// <summary>
        /// Converts the filter expression to a predicate suitable for Dynamic Linq e.g. "Field1 = @1 and Field2.Contains(@2)"
        /// </summary>
        /// <param name="filters">A list of flattened filters.</param>
        public string ToExpression(IList<Filter> filters)
        {
            if (Filters != null && Filters.Any())
            {
                return "(" + String.Join(" " + Logic + " ", Filters.Select(filter => filter.ToExpression(filters)).ToArray()) + ")";
            }

            int index = filters.IndexOf(this);

            string comparison = operators[Operator];

            if (Operator == "doesnotcontain")
            {
                return String.Format("!{0}.{1}(@{2})", Field, comparison, index);
            }

            if (comparison == "StartsWith" || comparison == "EndsWith" || comparison == "Contains")
            {
                return String.Format("{0}.{1}(@{2})", Field, comparison, index);
            }

            return String.Format("{0} {1} @{2}", Field, comparison, index);
        }
    }
}
