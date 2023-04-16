//#define MSDN_EXAMPLE
#define _CRT_SECURE_NO_WARNINGS
#define EXPORT extern "C" __declspec(dllexport)
#include <string>
#include <vector>
#include <iostream>
#include <stdio.h>
#include <comdef.h> //important
#include <objbase.h> // for returning
#include <strsafe.h> //arrays and strings safely
using namespace std;
 struct Geometry {
    double* vertices;
    unsigned int* indices;
    ~Geometry() {
        delete[] vertices;
        delete[] indices;
    }
};
 struct Segment {
    int nodeId;
    Segment* subsegments;
    Geometry* geometries;
    ~Segment() {
        //delete [] subsegments;
        //delete[] geometries;
        for (; subsegments; ++subsegments)
            CoTaskMemFree(subsegments);
        for (; geometries; ++geometries)
            CoTaskMemFree(geometries);
    }
};
vector<Segment*> Segments;
EXPORT void SegmentsInit() {
    Segment *seg1, *seg2, *seg3;
    seg1 = (Segment*)CoTaskMemAlloc(1 * sizeof(Segment));
    seg2 = (Segment*)CoTaskMemAlloc(1 * sizeof(Segment));
    seg3 = (Segment*)CoTaskMemAlloc(1 * sizeof(Segment));
    seg1->nodeId = 10; seg2->nodeId = 20; seg3->nodeId = 30;
    seg2->geometries = (Geometry*)(CoTaskMemAlloc(1 * sizeof(Geometry)));
    seg2->geometries[0].vertices = new double[] {1,2,3,4,5,6};
    seg2->geometries[0].indices = new unsigned int[] {0, 1};
    seg3->geometries = (Geometry*)(CoTaskMemAlloc(1 * sizeof(Geometry)));
    seg3->geometries[0].vertices = new double[] {2,4,6,8,10,12};
    seg3->geometries[0].indices = new unsigned int[] {2,3};
    seg3->subsegments = (Segment*)CoTaskMemAlloc(1 * sizeof(Segment));
    seg3->subsegments[0] = *seg2;

    Segments.push_back(seg1);
    Segments.push_back(seg2);
    Segments.push_back(seg3);
}
EXPORT void FreeResources() {
    for (Segment* s : Segments)
        //delete s;
        CoTaskMemFree(s);
    Segments.clear();
}
EXPORT Segment* GetASegment(int pos) {
    //if (Segments.size() > 0)
        return Segments[pos];
    //else {
        //cout << "No segments inited\n";
        //return nullptr;
    //}
}
EXPORT 
#pragma region declarations
EXPORT int Add(int, int);
EXPORT char myToUpper(wchar_t);
EXPORT double Divide(double, double);
EXPORT void PrintArray(int*, int);
EXPORT int* GetArrayPtr(int);
EXPORT void SetArray(int*,const int);
EXPORT void SetNewArray(int**, const int);
EXPORT int* GetArray(int);
EXPORT void ClearArray(int[]);
EXPORT BSTR RetString();
EXPORT const char* GetNewString(const char*, int);
EXPORT int* GetNewArray(const double*, int);

#pragma endregion
#pragma region implementations
char myToUpper(wchar_t ch) {
	return std::toupper(ch);
}
int Add(int a, int b) {
	return a + b;
}
double Divide(double a, double b) {
	return a / b;
}
void PrintArray(int* a,int a_size) {
	for (int i = 0; i < a_size; ++i)
		cout << *(a + i) << " ";
	cout << "\n";
}
int* GetArrayPtr(int a_size) {
	int* a = new int[a_size]{};
	return a;
}
void SetArray(int* a,const int a_size) {
	for (int i = a_size-1; i > 0; --i)
		a[i] = i * 3 - 5;
}
void ClearArray(int arr[]){
	delete arr;
}
int* GetArray(int size) {
	int* c = new int[size] {};
	for (int i = 0; i < size; ++i)
		c[i] = (i + 3) * 2 - 10;
	return c;
}
void SetNewArray(int** source,const int size) {
    CoTaskMemFree(*source);
    *source = (int*)CoTaskMemAlloc(size * sizeof(int));
    int now = 1, prev = 0, tmp = 0;
    for (int i = 0; i < size; i++)
    {
        *source[i] = now;
        tmp = now;
        now += prev;
        prev = tmp;
    }
}
BSTR RetString() {
	BSTR a = SysAllocString(L"Tobol");
	return a;
}
const char* GetNewString(const char* source, int len) {
    char* newStr = (char*)CoTaskMemAlloc((len + 1) * sizeof(char));
    for (int i = 0; i < len; ++i)
        newStr[i] = 'a' + i;
    newStr[len] = 0;
    return reinterpret_cast<const char*>(newStr);
}

int* GetNewArray(const double* source, int size) {
    int* newArray = reinterpret_cast<int*>(
        CoTaskMemAlloc(size*sizeof(int))
        );
    int prev = 0;
    int now = 1;
    int tmp = 0;
    std::cout << "C++: ";
    for (int i = 0; i < size; ++i)
    {
        newArray[i] = now;
        std::cout << now << " ";
        tmp = now;
        now += prev;
        prev = tmp;
        
    }
    cout << "\n";
    return newArray;
}

#pragma endregion

#ifdef MSDN_EXAMPLE

typedef struct _MYPOINT
{
	int x;
	int y;
} MYPOINT;

typedef struct _MYPERSON
{
	char* first;
	char* last;
} MYPERSON;

#define COL_DIM	1
EXPORT int TestArrayOfInts(int* pArray, int pSize);
EXPORT int TestRefArrayOfInts(int** ppArray, int* pSize);
EXPORT int TestMatrixOfInts(int pMatrix[][COL_DIM], int row);
EXPORT int TestArrayOfStrings(char** ppStrArray, int size);
EXPORT int TestArrayOfStructs(MYPOINT* pPointArray, int size);
EXPORT int TestArrayOfStructs2(MYPERSON* pPersonArray, int size);

//******************************************************************
int TestArrayOfInts(int* pArray, int size)
{
    int result = 0;

    for (int i = 0; i < size; i++)
    {
        result += pArray[i];
        pArray[i] += 100;
    }
    return result;
}

//******************************************************************
int TestRefArrayOfInts(int** ppArray, int* pSize)
{
    int result = 0;

    // CoTaskMemAlloc must be used instead of the new operator
    // because code on the managed side will call Marshal.FreeCoTaskMem
    // to free this memory.

    int* newArray = (int*)CoTaskMemAlloc(sizeof(int) * 5);

    for (int i = 0; i < *pSize; i++)
    {
        result += (*ppArray)[i];
    }

    for (int j = 0; j < 5; j++)
    {
        newArray[j] = (*ppArray)[j] + 100;
    }

    CoTaskMemFree(*ppArray);
    *ppArray = newArray;
    *pSize = 5;

    return result;
}

//******************************************************************
int TestMatrixOfInts(int pMatrix[][COL_DIM], int row)
{
    int result = 0;

    for (int i = 0; i < row; i++)
    {
        for (int j = 0; j < COL_DIM; j++)
        {
            result += pMatrix[i][j];
            pMatrix[i][j] += 100;
        }
    }
    return result;
}

//******************************************************************
int TestArrayOfStrings(char** ppStrArray, int count)
{
    int result = 0;
    STRSAFE_LPSTR temp;
    size_t len;
    const size_t alloc_size = sizeof(char) * 10;

    for (int i = 0; i < count; i++)
    {
        len = 0;
        StringCchLengthA(ppStrArray[i], STRSAFE_MAX_CCH, &len);
        result += len;

        temp = (STRSAFE_LPSTR)CoTaskMemAlloc(alloc_size);
        StringCchCopyA(temp, alloc_size, (STRSAFE_LPCSTR)"123456789");

        // CoTaskMemFree must be used instead of delete to free memory.

        CoTaskMemFree(ppStrArray[i]);
        ppStrArray[i] = (char*)temp;
    }

    return result;
}

//******************************************************************
int TestArrayOfStructs(MYPOINT* pPointArray, int size)
{
    int result = 0;
    MYPOINT* pCur = pPointArray;

    for (int i = 0; i < size; i++)
    {
        result += pCur->x + pCur->y;
        pCur->y = 0;
        pCur++;
    }

    return result;
}

//******************************************************************
int TestArrayOfStructs2(MYPERSON* pPersonArray, int size)
{
    int result = 0;
    MYPERSON* pCur = pPersonArray;
    STRSAFE_LPSTR temp;
    size_t len;

    for (int i = 0; i < size; i++)
    {
        len = 0;
        StringCchLengthA(pCur->first, STRSAFE_MAX_CCH, &len);
        len++;
        result += len;
        len = 0;
        StringCchLengthA(pCur->last, STRSAFE_MAX_CCH, &len);
        len++;
        result += len;

        len = sizeof(char) * (len + 2);
        temp = (STRSAFE_LPSTR)CoTaskMemAlloc(len);
        StringCchCopyA(temp, len, (STRSAFE_LPCSTR)"Mc");
        StringCbCatA(temp, len, (STRSAFE_LPCSTR)pCur->last);
        result += 2;

        // CoTaskMemFree must be used instead of delete to free memory.
        CoTaskMemFree(pCur->last);
        pCur->last = (char*)temp;
        pCur++;
    }

    return result;
}
#endif