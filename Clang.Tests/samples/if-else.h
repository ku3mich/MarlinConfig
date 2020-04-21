#if ENABLED(SDSUPPORT)
#define BLOCK_BUFFER_SIZE 16 // SD,LCD,Buttons take more memory, block buffer needs to be smaller
#else
#define BLOCK_BUFFER_SIZE 16 // maximize block buffer
#endif
