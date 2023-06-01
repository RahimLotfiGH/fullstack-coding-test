using AppTest1.Nodes;

namespace AppTest1.Helpers
{
    public class SampleFeedHelper
    {
        public static Tree CreateTree()
        {
            var treeArray = GetNodes();

            return TreeConvertor.ConvertToTree(treeArray);
        }

        public static Node[] GetNodes()
        {
            return new Node[] {

                new Node { Lable= 1, ParentLable=null },
                new Node { Lable= 2, ParentLable= 1 },
                new Node { Lable= 3, ParentLable= 2 },
                new Node { Lable= 4, ParentLable= 2 },
                new Node { Lable = 5, ParentLable= 1 },
                new Node { Lable = 6, ParentLable= 1 },
                new Node { Lable = 7, ParentLable= 6 },
                new Node { Lable = 8, ParentLable= 6 },
                new Node { Lable = 9, ParentLable= 8 }

            };
        }
    }
}
