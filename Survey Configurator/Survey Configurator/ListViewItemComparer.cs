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
                ListViewItem item1= (ListViewItem)pFirstQuestion;
                ListViewItem item2 = (ListViewItem)pSecondQuestion;

                //then cast the tag to a question type object
                Question question1 = (Question)item1.Tag;
                Question question2 = (Question)item2.Tag;

                //first check the sort order, then check which attribute do want to sort on
                if(SortingOrder == SortOrder.Ascending)
                {
                    switch ((ColumnsNames)ColumnIndex)
                    {
                        case ColumnsNames.Order:
                            return CompareNumbers(question1.Order, question2.Order);
                        case ColumnsNames.Text:
                            return String.Compare(question1.Text, question2.Text);
                        case ColumnsNames.Type:
                            return String.Compare(question1.Type.ToString(), question2.Type.ToString());
                        default:
                            return 0;
                    }
                }
                else
                {
                    switch ((ColumnsNames)ColumnIndex)
                    {
                        case ColumnsNames.Order:
                            return CompareNumbers(question2.Order, question1.Order);
                        case ColumnsNames.Text:
                            return String.Compare(question2.Text, question1.Text);
                        case ColumnsNames.Type:
                            return String.Compare(question2.Type.ToString(), question1.Type.ToString());
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

        private static int CompareNumbers(int number1, int number2)
        {
            try
            {
                if (number1 > number2)
                {
                    return 1;
                }
                else if (number1 < number2)
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
