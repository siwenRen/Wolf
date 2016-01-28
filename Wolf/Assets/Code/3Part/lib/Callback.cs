using UnityEngine;
using System.Collections;

public delegate void Callback();
public delegate void Callback<T>(T arg1);
public delegate void Callback<T, U>(T arg1, U arg2);
public delegate void Callback<T, U, V>(T arg1, U arg2, V arg3);
public delegate void Callback<T, U, V, Z>(T arg1, U arg2, V arg3, Z arg4);
public delegate void Callback<T, U, V, Z, S>(T arg1, U arg2, V arg3, Z arg4, S arg5);
public delegate void Callback<T, U, V, Z, S, W>(T arg1, U arg2, V arg3, Z arg4, S arg5, W arg6);
public delegate void Callback<T, U, V, Z, S, W, M>(T arg1, U arg2, V arg3, Z arg4, S arg5, W arg6, M arg7);
public delegate void Callback<T, U, V, Z, S, W, M, N>(T arg1, U arg2, V arg3, Z arg4, S arg5, W arg6, M arg7, N arg8);

