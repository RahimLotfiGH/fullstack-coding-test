
namespace AppTest1.Nodes
{
    public static class TreeConvertor
    {
        public static Tree ConvertToTree(this Node[] nodes)
        {
            var root = CreateRootTree(nodes);

            AddTreeNode(root, nodes);

            return root;
        }

        private static Tree CreateRootTree(Node[] nodes)
        {
            return new Tree
            {
                Lable = GetParentNode(nodes).Lable,
            };
        }

        private static Node GetParentNode(Node[] nodes)
        {
            return nodes.FirstOrDefault(p => p.ParentLable is null);

        }

        private static IEnumerable<Node> GetChildern(int lable, Node[] nodes)
        {
            return nodes.Where(p => p.ParentLable == lable).ToList();
        }

        private static Tree NodeToTree(Node node)
        {
            return new Tree
            {
                Lable = node.Lable,
            };
        }

        private static void AddTreeNode(Tree root, Node[] nodes)
        {
            var childern = GetChildern(root.Lable, nodes);

            if (!childern.Any()) return;

            root.Childern = new List<Tree>();

            foreach (var child in childern)
            {
                var childTree = NodeToTree(child);

                root.Childern.Add(childTree);

                AddTreeNode(childTree, nodes);
            }

        }

    }
}
