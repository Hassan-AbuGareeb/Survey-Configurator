using SharedResources;
using SharedResources.models;
using System.Collections;

namespace Survey_Configurator
{
     class ListViewItemComparer : IComparer
    {
        enum ColumnsNames{
             Order,
             Text,
             Type
        }

        private int ColumnIndex;
        private SortOrder SortingOrder;
        public ListViewItemComparer(int pColumn = 0, SortOrder pSortingOrder = SortOrder.Ascending)
        {
            try 
            { 
                ColumnIndex = pColumn;
                SortingOrder = pSortingOrder;
            }catch(Exception ex) 
            {
                UtilityMethods.LogError(ex);
            }
        }

        public int Compare(Object pFirstQuestion, Object pSecondQuestion)
        {
            try { 
                //cast the objects to ListView items first
                ListViewItem tItem1= (ListViewItem)pFirstQuestion;
                ListViewItem tItem2 = (ListViewItem)pSecondQuestion;

                //then cast the tag to a question type object
                Question tQuestion1 = (Question)tItem1.Tag;
                Question tQuestion2 = (Question)tItem2.Tag;

                //first check the sort order, then check which attribute do want to sort on
                if(SortingOrder == SortOrder.Ascending)
                {
                    switch ((ColumnsNames)ColumnIndex)
                    {
                        case ColumnsNames.Order:
                            return CompareNumbers(tQuestion1.Order, tQuestion2.Order);
                        case ColumnsNames.Text:
                            return String.Compare(tQuestion1.Text, tQuestion2.Text);
                        case ColumnsNames.Type:
                            return String.Compare(tQuestion1.Type.ToString(), tQuestion2.Type.ToString());
                        default:
                            return 0;
                    }
                }
                else
                {
                    switch ((ColumnsNames)ColumnIndex)
                    {
                        case ColumnsNames.Order:
                            return CompareNumbers(tQuestion2.Order, tQuestion1.Order);
                        case ColumnsNames.Text:
                            return String.Compare(tQuestion2.Text, tQuestion1.Text);
                        case ColumnsNames.Type:
                            return String.Compare(tQuestion2.Type.ToString(), tQuestion1.Type.ToString());
                        default:
                            return 0;
                    }
                }
            }catch (Exception ex)
            {
                UtilityMethods.LogError(ex);
                return 0;
            }
        }

        private static int CompareNumbers(int pNumber1, int pNumber2)
        {
            try
            {
                if (pNumber1 > pNumber2)
                {
                    return 1;
                }
                else if (pNumber1 < pNumber2)
                {
                    return -1;
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                UtilityMethods.LogError(ex);
                return 0;
            }
        }
    }
}
