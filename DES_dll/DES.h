#pragma once
/**
*
*	函数:  DES_encrypt(unsigned k1,unsigned k2,unsigned m1,unsigned m2,unsigned& c1,unsigned& c2)
*
*	功能：使用密钥k1k2采用DES加密算法加密m1m2中存储的数据
*
*	参数：k1 密钥低4个字节。
*	参数：k2 密钥高4个字节。
*	参数：m1 明文低4个字节
*	参数：m2 明文高4个字节
*	参数：c1 密文低4个字节。
*	参数：c2 密文高4个字节。
*/

void DES_encrypt(unsigned k1, unsigned k2, unsigned m1, unsigned m2, unsigned& c1, unsigned& c2);

/**
*
*	函数: DES_decrypt(unsigned k1,unsigned k2,unsigned c1,unsigned c2,unsigned& m1,unsigned& m2)
*
*	功能：使用密钥k1k2采用DES加密算法解密c1c2中存储的数据
*
*	参数：k1 密钥低4个字节。
*	参数：k2 密钥高4个字节。
*	参数：c1 密文低4个字节。
*	参数：c2 密文高4个字节。
*	参数：m1 明文低4个字节
*	参数：m2 明文高4个字节
*/

void DES_decrypt(unsigned k1, unsigned k2, unsigned c1, unsigned c2, unsigned& m1, unsigned& m2);



/*
	下面是对加密/解密函数的简单封装
*/
// 默认为大端存储，大端存储即数据的高字节保存在内存的低地址。
// 但是x86采用的是小端存储。
typedef unsigned int uint;
typedef unsigned char uchar;

void bytes2uint(uchar bytes[4], uint& u32);

void uint2bytes(uint u32, uchar bytes[4]);

// 加密函数的简答封装
/*--------------------------------------------------*/
/*          data encryption (des加密)               */
/*  参数 m_bit[8]:明文分组    k_bit[8]:密钥     e_bit[8]:密文分组 */
/*--------------------------------------------------*/
extern "C" __declspec(dllexport) void __stdcall endes(unsigned char m_bit[8], unsigned char k_bit[8], unsigned char e_bit[8]);

// 解密函数的简答封装
/*--------------------------------------------------              */
/*                  data uncryption (des解密)                       */
/*  参数 m_bit[8]:密文分组    k_bit[8]:密钥     e_bit[8]:明文分组 */
/*--------------------------------------------------              */
extern "C" __declspec(dllexport) void __stdcall undes(unsigned char m_bit[8], unsigned char k_bit[8], unsigned char e_bit[8]);

extern "C" __declspec(dllexport) int __stdcall add(int a, int b);