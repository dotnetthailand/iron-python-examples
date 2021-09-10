class Math:

    def sum_method(self, value1, value2):
        return sum_function(value1, value2)

def sum_function(value1, value2):
    return value1 + value2


def string_multiply(input_string, times):
    return input_string * times

# ipyc /target:dll Math.py
