
namespace AppTest1.Nodes
{
    public static class NodeConvertor
    {

        public static List<Node> ConvertNodeFromTree(this Tree root)
        {
            var nodes = new List<Node>();

            if (root is null) return nodes;

            AddTreeToArray(root, null, nodes);

            return nodes;
        }

        private static void AddTreeToArray(Tree tree, int? parntLable, List<Node> nodes)
        {
            var node = CreateNodeFromTree(tree, parntLable);

            nodes.Add(node);

            if (tree.Childern is null) return;

            foreach (var child in tree.Childern)
                AddTreeToArray(child, tree.Lable, nodes);

        }

        private static Node CreateNodeFromTree(Tree tree, int? parntLable)
        {
            return new Node
            {
                Lable = tree.Lable,
                ParentLable = parntLable
            };
        }

    }
}
