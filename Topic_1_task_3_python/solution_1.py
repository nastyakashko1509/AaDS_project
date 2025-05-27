class TreeNode:
    def __init__(self, key):
        self.key = key
        self.left = None
        self.right = None

def count_nodes(node):
    if node is None:
        return 0
    return 1 + count_nodes(node.left) + count_nodes(node.right)

def find_candidate_nodes(node, candidates):
    if node is None:
        return
    left_count = count_nodes(node.left)
    right_count = count_nodes(node.right)
    if abs(left_count - right_count) == 1:
        candidates.append(node)
    find_candidate_nodes(node.left, candidates)
    find_candidate_nodes(node.right, candidates)

def delete_node(root, key):
    if root is None:
        return root
    
    if key < root.key:
        root.left = delete_node(root.left, key)
    elif key > root.key:
        root.right = delete_node(root.right, key)
    else:
        # Узел с одним потомком или без потомков
        if root.left is None:
            return root.right
        elif root.right is None:
            return root.left
        
        # Узел с двумя потомками: находим минимальный в правом поддереве
        temp = root.right
        while temp.left is not None:
            temp = temp.left
        
        # Копируем значение минимального узла
        root.key = temp.key
        # Удаляем минимальный узел
        root.right = delete_node(root.right, temp.key)
    
    return root

def preorder_traversal(node):
    if node is None:
        return []
    return [node.key] + preorder_traversal(node.left) + preorder_traversal(node.right)

def solve_task(root):
    candidates = []
    find_candidate_nodes(root, candidates)
    
    if not candidates:
        print("Нет подходящих вершин для удаления")
        return preorder_traversal(root)
    
    # Сортируем кандидатов по значению ключа
    candidates.sort(key=lambda x: x.key)
    
    # Находим среднюю по значению вершину
    middle_index = len(candidates) // 2
    middle_node = candidates[middle_index]
    
    # Удаляем вершину (сохраняем ссылку на корень)
    if root == middle_node:
        # Если удаляем корень, нужно найти новый корень
        if root.left is None:
            root = root.right
        elif root.right is None:
            root = root.left
        else:
            temp = root.right
            while temp.left is not None:
                temp = temp.left
            root.key = temp.key
            root.right = delete_node(root.right, temp.key)
    else:
        delete_node(root, middle_node.key)
    
    return preorder_traversal(root)


def input_tree():
    print("Введите дерево в формате:")
    print("родитель левый_потомок правый_потомок")
    print("Вводите по одному узлу на строку, для завершения введите пустую строку")
    print("Пример:")
    print("10 5 15")
    print("5 2 7")
    print("15 None 20")
    print("20 18 None")
    
    nodes = {}
    root = None
    
    while True:
        line = input().strip()
        if not line:
            break
        
        try:
            parent_val, left_val, right_val = line.split()
        except ValueError:
            print("Ошибка формата, используйте 3 значения через пробел")
            continue
        
        # Обработка значений
        parent_val = int(parent_val)
        left_val = None if left_val == "None" else int(left_val)
        right_val = None if right_val == "None" else int(right_val)
        
        # Создаем/получаем родительский узел
        if parent_val not in nodes:
            nodes[parent_val] = TreeNode(parent_val)
            if root is None:  # Первый узел - корень
                root = nodes[parent_val]
        
        # Обрабатываем левого потомка
        if left_val is not None:
            if left_val not in nodes:
                nodes[left_val] = TreeNode(left_val)
            nodes[parent_val].left = nodes[left_val]
        
        # Обрабатываем правого потомка
        if right_val is not None:
            if right_val not in nodes:
                nodes[right_val] = TreeNode(right_val)
            nodes[parent_val].right = nodes[right_val]
    
    return root