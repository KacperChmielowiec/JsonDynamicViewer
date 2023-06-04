// ##########################################
// Solution: KTR
// Project: KTR.Main
// File: TreeView4TierViewModel.cs
// 
// Last User: Chris Hildebran
// Last Edit: 2019-02-18 9:39 AM
// ##########################################
namespace KTR.Main.TreeViewAllElementInstances.VM
{
	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Diagnostics;
	using System.Linq;
	using System.Text;

	using Autodesk.Revit.ApplicationServices;
	using Autodesk.Revit.DB;
	using Autodesk.Revit.UI;

	using KTR.Main.TreeViewAllElementInstances.M;
	using KTR.Utility;
	using KTR.Utility.Collectors;
	using KTR.Utility.Types;

	public class TreeView4TierViewModel
	{
		#region Fields

		private static List<Tuple<Category, string, Type, Element>> _allInstanceNames;

		private static ExternalCommandData _commandData;

		private static List<Category> _distinctCategoryNames;

		private static List<Tuple<Category, string>> _distinctFamilyNames;

		private static List<Tuple<Category, string, Type>> _distinctTypeNames;

		private Application _revitApp;

		private static Document _revitDoc;

		private UIApplication _revitUiApp;

		private UIDocument _revitUiDoc;

		#endregion

		#region Constructors

		public TreeView4TierViewModel()
		{
		}




		public TreeView4TierViewModel(ExternalCommandData commandData)
		{
			{
				_commandData = commandData;
				_revitApp    = commandData.Application.Application;
				_revitDoc    = commandData.Application.ActiveUIDocument.Document;
				_revitUiApp  = commandData.Application;
				_revitUiDoc  = commandData.Application.ActiveUIDocument;
			}

			ProjectName = _revitDoc.ProjectInformation.Name;

			var sw1 = new Stopwatch();
			sw1.Start();
			IEnumerable<Element> elements = GetInstancesInProject();
			sw1.Stop();

			Seconds1 = "[Coll. & Tup.: " + Math.Round(sw1.Elapsed.TotalSeconds, 3) + " Seconds]";

			ProcessElementInstances(elements);


			//var sw3 = new Stopwatch();
			//sw3.Start();
			//IEnumerable<RevitElementInstance> elementInstances = GetInstancesInProjectNew();
			//sw3.Stop();

			//Seconds3 = "[Coll. & Obj.: " + Math.Round(sw3.Elapsed.TotalSeconds, 3) + " Seconds]";

			//ProcessElementInstancesNew(elementInstances);

			var sw2 = new Stopwatch();
			sw2.Start();
			InitializeDataSource();
			sw2.Stop();

			Seconds2 = Math.Round(sw2.Elapsed.TotalSeconds, 3) + " Seconds";
		}

		#endregion

		#region Properties

		public static int CollObjCount
		{
			get;
			set;
		}

		public static int CollTupCount
		{
			get;
			set;
		}

		public static string ProjectName
		{
			get;

			set;
		}

		public string Seconds1
		{
			get;
		}

		public string Seconds2
		{
			get;
		}

		public string Seconds3
		{
			get;
			set;
		}

		public static ObservableCollection<Tier1Object> TreeViewCollection
		{
			get;
			set;
		}

		#endregion

		#region Methods

		private static IEnumerable<Element> GetInstancesInProject()
		{
			IEnumerable<Element> elements = ProjectScope.GetElementInstancesWithTypeAndCategory(_revitDoc);

			CollTupCount = elements.Count();

			return elements;
		}




		private static IEnumerable<RevitElementInstance> GetInstancesInProjectNew()
		{
			IEnumerable<RevitElementInstance> elementInstances = RevitElementInstance.GetBasics(_revitDoc);

			IEnumerable<RevitElementInstance> elements = elementInstances.ToList();

			CollObjCount = elements.Count();

			return elements;
		}




		private static void InitializeDataSource()
		{
			TreeViewCollection = new ObservableCollection<Tier1Object>();
			Tier1Object tier1Object;
			Tier2Object tier2Object;
			Tier3Object tier3Object;
			Tier4Object tier4Object;


			// ##################################
			// ########## Tier 1 Start ##########
			// ##################################

			foreach(var category in _distinctCategoryNames)
			{
				// Add Tier 1 Item - 2019-02-11 10:15am
				TreeViewCollection.Add(tier1Object = new Tier1Object(category.Name));


				// ##################################
				// ########## Tier 2 Start ##########
				// ##################################

				// List Of Family Names Filtered On Current Category - 2019-02-11 10:15am
				IEnumerable<Tuple<Category, string>> tier2Tuples = _distinctFamilyNames.Where(tuple => tuple.Item1.Name == category.Name);


				// For each Tuple in Tuple List.... - 2019-02-11 10:15am
				foreach(Tuple<Category, string> tier2Tuple in tier2Tuples)
				{
					// Add Tier 2 Item - 2019-02-11 10:15am
					tier1Object.Tier2FamilyNames.Add(tier2Object = new Tier2Object(tier2Tuple.Item2));


					// ##################################
					// ########## Tier 3 Start ##########
					// ##################################

					// List Of Type Names Filtered On Current Family Name - 2019-02-11 10:15am
					IEnumerable<Tuple<Category, string, Type>> tier3Tuples = _distinctTypeNames.Where(tuple => tuple.Item1 + tuple.Item2 == category + tier2Tuple.Item2);


					// For each Tuple in Tuple List.... - 2019-02-11 10:15am
					foreach(Tuple<Category, string, Type> tier3Tuple in tier3Tuples)
					{
						// Add Tier 3 Item
						tier2Object.Tier3TypeNames.Add(tier3Object = new Tier3Object(tier3Tuple.Item3.Name));


						// ##################################
						// ########## Tier 4 Start ##########
						// ##################################

						// List Of Instance Names Filtered On Current Type Name - 2019-02-11 10:15am
						IEnumerable<Tuple<Category, string, Type, Element>> tier4Tuples = _allInstanceNames.Where(tuple => tuple.Item1 + tuple.Item2 + tuple.Item3 == category + tier2Tuple.Item2 + tier3Tuple.Item3);

						foreach(Tuple<Category, string, Type, Element> tier4Tuple in tier4Tuples)
						{
							// Add Tier 4 Item
							tier3Object.Tier4ElementInstanceItem.Add(tier4Object = new Tier4Object(tier4Tuple.Item4.Name));
						}
					}
				}
			}
		}




		private static void ProcessElementInstances(IEnumerable<Element> elements)
		{
			List<Category>                               t1DistinctCategoryNames = new List<Category>();
			List<Tuple<Category, string>>                t2DistinctFamilyNames   = new List<Tuple<Category, string>>();
			List<Tuple<Category, string, Type>>          t3DistinctTypeNames     = new List<Tuple<Category, string, Type>>();
			List<Tuple<Category, string, Type, Element>> t4AllInstanceNames      = new List<Tuple<Category, string, Type, Element>>();

			foreach(var element in elements)
			{
				// Common Variables
				var elementType = _revitDoc.GetElement(element.GetTypeId()) as ElementType;


				// Tier 1 - Category
				var t1CategoryName = element.Category;

				if(!t1DistinctCategoryNames.Exists(category => category.Name.ToString().Equals(t1CategoryName.Name.ToString())))
				{
					t1DistinctCategoryNames.Add(t1CategoryName);
				}


				// Tier 2 - Family Name
				var t2FamilyName = elementType.FamilyName;

				if(!t2DistinctFamilyNames.Exists(tuple => (tuple.Item1.Name + tuple.Item2).Equals(t1CategoryName.Name + t2FamilyName)))
				{
					t2DistinctFamilyNames.Add(new Tuple<Category, string>(t1CategoryName, t2FamilyName));
				}


				// Tier 3 - Type Name
				var t3TypeName = element.GetType();

				if(!t3DistinctTypeNames.Exists(tuple => (tuple.Item1.Name + tuple.Item2 + tuple.Item3.Name).Equals(t1CategoryName.Name + t2FamilyName + t3TypeName.Name)))
				{
					t3DistinctTypeNames.Add(new Tuple<Category, string, Type>(t1CategoryName, t2FamilyName, t3TypeName));
				}


				// Tier 4 - Instance Name
				var t4InstName = element;

				t4AllInstanceNames.Add(new Tuple<Category, string, Type, Element>(t1CategoryName, t2FamilyName, t3TypeName, t4InstName));
			}

			_distinctCategoryNames = t1DistinctCategoryNames.OrderBy(category => category.Name.ToString()).ToList();

			_distinctFamilyNames = t2DistinctFamilyNames.OrderBy(tuple => tuple.Item1.Name.ToString()).ThenBy(tuple => tuple.Item2).ToList();

			_distinctTypeNames = t3DistinctTypeNames.OrderBy(tuple => tuple.Item1.Name).ThenBy(tuple => tuple.Item2).ThenBy(tuple => tuple.Item3.Name).ToList();

			_allInstanceNames = t4AllInstanceNames.OrderBy(tuple => tuple.Item1.Name).ThenBy(tuple => tuple.Item2).ThenBy(tuple => tuple.Item3.Name).ThenBy(tuple => tuple.Item4.Name).ToList();

			// WriteToDevelopmentTextFile();
		}




		//private static void ProcessElementInstancesNew(IEnumerable<RevitElementInstance> elementInstances)
		//{
		//	List<Category>                               t1DistinctCategoryNames = new List<Category>();
		//	List<Tuple<Category, string>>                t2DistinctFamilyNames   = new List<Tuple<Category, string>>();
		//	List<Tuple<Category, string, Type>>          t3DistinctTypeNames     = new List<Tuple<Category, string, Type>>();
		//	List<Tuple<Category, string, Type, Element>> t4AllInstanceNames      = new List<Tuple<Category, string, Type, Element>>();

		//	foreach(var elementInstance in elementInstances)
		//	{
		//		// Tier 1 - Category
		//		var t1CategoryName = elementInstance.Category;

		//		if(!t1DistinctCategoryNames.Exists(category => category.Name.ToString().Equals(t1CategoryName.Name.ToString())))
		//		{
		//			t1DistinctCategoryNames.Add(t1CategoryName);
		//		}

		//		// Tier 2 - Family Name
		//		var t2FamilyName = elementInstance.FamilyName;

		//		if(!t2DistinctFamilyNames.Exists(tuple => (tuple.Item1.Name + tuple.Item2).Equals(t1CategoryName.Name + t2FamilyName)))
		//		{
		//			t2DistinctFamilyNames.Add(new Tuple<Category, string>(t1CategoryName, t2FamilyName));
		//		}

		//		// Tier 3 - Type Name
		//		var t3TypeName = elementInstance.Type;

		//		if(!t3DistinctTypeNames.Exists(tuple => (tuple.Item1.Name + tuple.Item2 + tuple.Item3.Name).Equals(t1CategoryName.Name + t2FamilyName + t3TypeName.Name)))
		//		{
		//			t3DistinctTypeNames.Add(new Tuple<Category, string, Type>(t1CategoryName, t2FamilyName, t3TypeName));
		//		}

		//		// Tier 4 - Instance Name
		//		var t4InstanceName = elementInstance.ElementInstance;

		//		t4AllInstanceNames.Add(new Tuple<Category, string, Type, Element>(t1CategoryName, t2FamilyName, t3TypeName, t4InstanceName));
		//	}

		//	_distinctCategoryNames = t1DistinctCategoryNames.OrderBy(category => category.Name.ToString()).ToList();

		//	_distinctFamilyNames = t2DistinctFamilyNames.OrderBy(tuple => tuple.Item1.Name.ToString()).ThenBy(tuple => tuple.Item2).ToList();

		//	_distinctTypeNames = t3DistinctTypeNames.OrderBy(tuple => tuple.Item1.Name).ThenBy(tuple => tuple.Item2).ThenBy(tuple => tuple.Item3.Name).ToList();

		//	_allInstanceNames = t4AllInstanceNames.OrderBy(tuple => tuple.Item1.Name).ThenBy(tuple => tuple.Item2).ThenBy(tuple => tuple.Item3.Name).ThenBy(tuple => tuple.Item4.Name).ToList();
		//}




		private static void WriteToDevelopmentTextFile()
		{
			// Tier 1 - Category Send To TextFile
			var t1CategoryNameTextFile = new StringBuilder();

			foreach(var category in _distinctCategoryNames)
			{
				t1CategoryNameTextFile.AppendLine(category.Name);
			}

			File.WriteDevelopmentListToTextFile(_revitDoc, t1CategoryNameTextFile, "T1 Category Names");


			// Tier 2 - Family Name Send To TextFile
			var t2FamilyNameTextFile = new StringBuilder();

			foreach(Tuple<Category, string> tuple in _distinctFamilyNames)
			{
				t2FamilyNameTextFile.AppendLine(tuple.Item1.Name + ", " + tuple.Item2);
			}

			File.WriteDevelopmentListToTextFile(_revitDoc, t2FamilyNameTextFile, "T2 Family Names");


			// Tier 3 - Family Name Send To TextFile
			var t3TypeNameTextFile = new StringBuilder();

			foreach(Tuple<Category, string, Type> tuple in _distinctTypeNames)
			{
				t3TypeNameTextFile.AppendLine(tuple.Item1.Name + ", " + tuple.Item2 + ", " + tuple.Item3.Name);
			}

			File.WriteDevelopmentListToTextFile(_revitDoc, t3TypeNameTextFile, "T3 Type Names");


			// Tier 4 - Instance Name Send To TextFile
			var t4InstNameTextFile = new StringBuilder();

			foreach(Tuple<Category, string, Type, Element> tuple in _allInstanceNames)
			{
				t4InstNameTextFile.AppendLine(tuple.Item1.Name + ", " + tuple.Item2 + ", " + tuple.Item3.Name + ", " + tuple.Item4.Name);
			}

			File.WriteDevelopmentListToTextFile(_revitDoc, t4InstNameTextFile, "T4 Instance Names");
		}

		#endregion
	}
}