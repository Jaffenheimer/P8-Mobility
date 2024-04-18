import csv


def CSVWriter(data, filename, header):
    with open(filename, mode='w', newline='') as file:
        writer = csv.writer(file)
        writer.writerow(header)

        writer.writerows(zip(*data))

    print("File written successfully to: ", filename)