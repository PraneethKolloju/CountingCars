import { useParamsStore } from '@/hooks/useParamsStore'
import { Button } from 'flowbite-react'
import { stat } from 'fs'
import React from 'react'

type Props = {
    title?: string,
    subTitle?: string,
    showReset?: boolean
}

export default function EmptyFilter({
    title = "No matches for this filter",
    subTitle = "Try changing the filters",
    showReset
}: Props) {
    const reset = useParamsStore(state => state.reset);
    return (
        <div className='flex flex-col gap-2 items-center justify-center h-[40vh] shadow-lg'>
            <h1>{title}</h1>
            <h3>{subTitle}</h3>
            <div className='mt-3'>
                {showReset && (
                    <Button onClick={reset}>Reset filters</Button>
                )}
            </div>
        </div>
    )
}
