'use client';
import { Pagination } from 'flowbite-react'
import React, { useState } from 'react'

type Props =
    {
        currentPage: number,
        pageCount: number,
        onPageChange: (pageNumber: number) => void
    }

export default function AppPagination({ currentPage, pageCount, onPageChange }: Props) {
    return (
        <>
            <Pagination currentPage={currentPage}
                onPageChange={e => onPageChange(e)}
                totalPages={pageCount}
                layout='pagination'
                showIcons={true}
                className="text-blue-500 mb-5"
            />
        </>
    )
}
