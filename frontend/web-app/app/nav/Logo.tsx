'use client'

import { useParamsStore } from '@/hooks/useParamsStore'
import React from 'react'
import { AiOutlineCar } from 'react-icons/ai'

export default function Logo() {
    const reset = useParamsStore(state => state.reset);
    return (
        <>
            <div className="flex items-center gap-2 text-2xl font-semibold text-red-500" onClick={reset}>
                <AiOutlineCar size={34} />
                <h3>Counting Cars</h3>
            </div>
        </>
    )
}
