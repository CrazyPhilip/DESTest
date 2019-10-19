#pragma once
/**
*
*	����:  DES_encrypt(unsigned k1,unsigned k2,unsigned m1,unsigned m2,unsigned& c1,unsigned& c2)
*
*	���ܣ�ʹ����Կk1k2����DES�����㷨����m1m2�д洢������
*
*	������k1 ��Կ��4���ֽڡ�
*	������k2 ��Կ��4���ֽڡ�
*	������m1 ���ĵ�4���ֽ�
*	������m2 ���ĸ�4���ֽ�
*	������c1 ���ĵ�4���ֽڡ�
*	������c2 ���ĸ�4���ֽڡ�
*/

void DES_encrypt(unsigned k1, unsigned k2, unsigned m1, unsigned m2, unsigned& c1, unsigned& c2);

/**
*
*	����: DES_decrypt(unsigned k1,unsigned k2,unsigned c1,unsigned c2,unsigned& m1,unsigned& m2)
*
*	���ܣ�ʹ����Կk1k2����DES�����㷨����c1c2�д洢������
*
*	������k1 ��Կ��4���ֽڡ�
*	������k2 ��Կ��4���ֽڡ�
*	������c1 ���ĵ�4���ֽڡ�
*	������c2 ���ĸ�4���ֽڡ�
*	������m1 ���ĵ�4���ֽ�
*	������m2 ���ĸ�4���ֽ�
*/

void DES_decrypt(unsigned k1, unsigned k2, unsigned c1, unsigned c2, unsigned& m1, unsigned& m2);



/*
	�����ǶԼ���/���ܺ����ļ򵥷�װ
*/
// Ĭ��Ϊ��˴洢����˴洢�����ݵĸ��ֽڱ������ڴ�ĵ͵�ַ��
// ����x86���õ���С�˴洢��
typedef unsigned int uint;
typedef unsigned char uchar;

void bytes2uint(uchar bytes[4], uint& u32);

void uint2bytes(uint u32, uchar bytes[4]);

// ���ܺ����ļ���װ
/*--------------------------------------------------*/
/*          data encryption (des����)               */
/*  ���� m_bit[8]:���ķ���    k_bit[8]:��Կ     e_bit[8]:���ķ��� */
/*--------------------------------------------------*/
extern "C" __declspec(dllexport) void __stdcall endes(unsigned char m_bit[8], unsigned char k_bit[8], unsigned char e_bit[8]);

// ���ܺ����ļ���װ
/*--------------------------------------------------              */
/*                  data uncryption (des����)                       */
/*  ���� m_bit[8]:���ķ���    k_bit[8]:��Կ     e_bit[8]:���ķ��� */
/*--------------------------------------------------              */
extern "C" __declspec(dllexport) void __stdcall undes(unsigned char m_bit[8], unsigned char k_bit[8], unsigned char e_bit[8]);

extern "C" __declspec(dllexport) int __stdcall add(int a, int b);