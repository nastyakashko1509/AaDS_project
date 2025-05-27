class TreeNode:
    def __init__(self, value):
        self.value = value
        self.left = None
        self.right = None

def count_nodes(node):
    if node is None:
        return 0
    return 1 + count_nodes(node.left) + count_nodes(node.right)

def find_nodes_with_condition(node, nodes):
    if node is None:
        return
    left_count = count_nodes(node.left)
    right_count = count_nodes(node.right)
    if abs(left_count - right_count) == 1:
        nodes.append(node)
    find_nodes_with_condition(node.left, nodes)
    find_nodes_with_condition(node.right, nodes)

def find_min(node):
    while node.left is not None:
        node = node.left
    return node

def right_delete(root, value):
    if root is None:
        return root
    if value < root.value:
        root.left = right_delete(root.left, value)
    elif value > root.value:
        root.right = right_delete(root.right, value)
    else:
        if root.left is None:
            return root.right
        elif root.right is None:
            return root.left
        temp = find_min(root.right)
        root.value = temp.value
        root.right = right_delete(root.right, temp.value)
    return root

def preorder_traversal(node):
    if node is None:
        return []
    return [node.value] + preorder_traversal(node.left) + preorder_traversal(node.right)

def solve(root):
    nodes = []
    find_nodes_with_condition(root, nodes)
    if not nodes:
        print("Нет подходящих вершин для удаления.")
        return preorder_traversal(root)
    nodes.sort(key=lambda x: x.value)
    middle_index = len(nodes) // 2
    middle_node = nodes[middle_index]
    root = right_delete(root, middle_node.value)
    return preorder_traversal(root)

def input_tree():
    print("Введите дерево в формате:")
    print("левый_потомок корень правый_потомок")
    print("Для пустого узла используйте 'None'")
    print("Пример ввода: None 10 None - создает корень 10 без потомков")
    print("Введите 'end' для завершения ввода")

    nodes = {}  # Словарь для хранения узлов: {значение: узел}
    root = None

    while True:
        line = input().strip()
        if line.lower() == 'end':
            break

        left_val, parent_val, right_val = line.split()

        # Создаем/получаем родительский узел
        if parent_val not in nodes:
            nodes[parent_val] = TreeNode(int(parent_val))
            if root is None:  # Первый узел становится корнем
                root = nodes[parent_val]

        # Обрабатываем левого потомка
        if left_val != 'None':
            if left_val not in nodes:
                nodes[left_val] = TreeNode(int(left_val))
            nodes[parent_val].left = nodes[left_val]

        # Обрабатываем правого потомка
        if right_val != 'None':
            if right_val not in nodes:
                nodes[right_val] = TreeNode(int(right_val))
            nodes[parent_val].right = nodes[right_val]

    return root